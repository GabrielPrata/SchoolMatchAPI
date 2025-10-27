using AccountService.Data.DTO;
using AccountService.Repository.Queries;
using AccountService.Mappers;
using AccountService.Model.MongoModels;
using AccountService.Model.SqlModels;
using AccountService.Model.Base;
using MimeKit;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using System.Text.Json;

namespace AccountService.Service
{
    public class UserDataService : IUserDataService
    {
        private readonly UserDataSqlRepository _userSqlRepository;
        private readonly UserDataMongoRepository _userMongoRepository;
        private readonly EmailConfig _emailConfig;
        private readonly string _identityUrl;

        public UserDataService(string sqlConnection, string mongoConnection, IOptions<EmailConfig> emailConfig, string identityUrl)
        {
            _userSqlRepository = new UserDataSqlRepository(sqlConnection);
            _userMongoRepository = new UserDataMongoRepository(mongoConnection);
            _emailConfig = emailConfig.Value;
            _identityUrl = identityUrl;
        }

        public async Task<UserDataDTO> GetUserDataById(int userId)
        {
            try
            {
                #region Recuperando os dados do SQL
                var sqlModel = await _userSqlRepository.GetUserDataById(userId);
                IEnumerable<SqlBlocks> userBlocks = await _userSqlRepository.GetUserBlocksById(userId);
                IEnumerable<SqlPreferences> userPreferences = await _userSqlRepository.GetUserPreferencesById(userId);

                sqlModel.BlocosUsario = userBlocks.Where(b => b.BlocoPrincipal == false).Select(b => BlocksMapper.ToDto(b)).ToList();
                sqlModel.BlocoPrincipal = userBlocks.Where(b => b.BlocoPrincipal == true).Select(b => BlocksMapper.ToDto(b)).FirstOrDefault();
                sqlModel.UsuarioPreferenciaGenero = userPreferences.Select(PreferencesMapper.ToDto).ToList();

                var mongoModel = await _userMongoRepository.GetUserById(userId);



                if (sqlModel != null && mongoModel != null)
                {
                    var model = UserMapper.ToDto(sqlModel, mongoModel);
                    return model;
                }
                #endregion

                return null;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public async Task<UserDataDTO> ValidateLogin(UserLoginDTO loginData)
        {
            if (await _userSqlRepository.ValidateLogin(loginData))
            {
                try
                {
                    int userId = await _userSqlRepository.GetUserIdByEmail(loginData.Email);
                    SqlUserData sqlUserData = await _userSqlRepository.GetUserDataById(userId);
                    IEnumerable<SqlBlocks> userBlocks = await _userSqlRepository.GetUserBlocksById(userId);
                    IEnumerable<SqlPreferences> userPreferences = await _userSqlRepository.GetUserPreferencesById(userId);

                    sqlUserData.BlocosUsario = userBlocks.Where(b => b.BlocoPrincipal == false).Select(b => BlocksMapper.ToDto(b)).ToList();
                    sqlUserData.BlocoPrincipal = userBlocks.Where(b => b.BlocoPrincipal == true).Select(b => BlocksMapper.ToDto(b)).FirstOrDefault();
                    sqlUserData.UsuarioPreferenciaGenero = userPreferences.Select(PreferencesMapper.ToDto).ToList();          


                    MongoUserData mongoUserData = await _userMongoRepository.GetUserById(userId);

                    UserDataDTO userData = UserMapper.ToDto(sqlUserData, mongoUserData);

                    string userToken = await GetIdentityToken(userId, sqlUserData.EmailUsuario, sqlUserData.NomeUsuario);

                    userData.UserToken = userToken; 

                    return userData;
                }
                catch (Exception ex)
                {
                    throw new ApiException(new ApiErrorModel(message: "Um erro inesperado aconteceu:", statusCode: 500, stackTrace: ex.StackTrace));
                }
            }
            else
            {
                throw new ApiException(new ApiErrorModel(message: "Nenhum usuário encontrado com essas credenciais!", statusCode: 404));
            }
        }

        public async Task SaveUserData(UserDataDTO userData)
        {
            // TODO: separar a regra de negócio do repository:
            // TODO: Aplicar unitOfWork - Commit Transactions, caso alguma dessas operações de erro, execute um rollback.
            if (await _userSqlRepository.VerifyUserExist(userData.EmailUsuario))
            {
                var error = new ApiErrorModel("Este endereço de e-mail já está cadastrado em nosso sistema!", 409, Environment.StackTrace);
                throw new ApiException(error);
            }


            SqlUserData sqlData = userData.ToSqlModel();
            MongoUserData mongoData = userData.ToMongoModel();

            int userId = await _userSqlRepository.SaveUserData(sqlData);

            foreach (GenderDTO gender in sqlData.UsuarioPreferenciaGenero)
            {
                await _userSqlRepository.SaveUserGenreInterests(userId, gender.GenderId);
            }

            sqlData.BlocosUsario.Add(sqlData.BlocoPrincipal);
            foreach (BlocksDTO block in sqlData.BlocosUsario)
            {
                await _userSqlRepository.SaveUserBlocks(userId, block.BlockId);
            }
            mongoData.IdUsuario = userId;
            await _userMongoRepository.SaveUserData(mongoData);
        }

        public async Task UpdateUserData(UserDataDTO userData)
        {
            if (!userData.IdUsuario.HasValue || !await _userSqlRepository.VerifyUserExist(userData.EmailUsuario))
            {
                var error = new ApiErrorModel("Usuário não encontrado no sistema!", 404, Environment.StackTrace);
                throw new ApiException(error);
            }

            int userId = userData.IdUsuario.Value;

            userData.UsuarioEditedAt = DateTime.Now;

            SqlUserData sqlData = userData.ToSqlModel();
            MongoUserData mongoData = userData.ToMongoModel();

            await _userSqlRepository.UpdateUserData(sqlData);

            await _userSqlRepository.DeleteUserGenreInterests(userId);
            foreach (GenderDTO gender in sqlData.UsuarioPreferenciaGenero)
            {
                await _userSqlRepository.SaveUserGenreInterests(userId, gender.GenderId);
            }

            await _userSqlRepository.DeleteUserBlocks(userId);
            sqlData.BlocosUsario.Add(sqlData.BlocoPrincipal);
            foreach (BlocksDTO block in sqlData.BlocosUsario)
            {
                await _userSqlRepository.SaveUserBlocks(userId, block.BlockId);
            }

            await _userMongoRepository.UpdateUserData(mongoData);
        }

        public async Task SaveEmailToVerify(string userEmail)
        {
            //TODO: Criar classe static paa retornar as mensagens, Ex: ConstanteMensagens.EmailNaoCadastrado
            if (await _userSqlRepository.CheckIfEmailIsVerified(userEmail))
            {
                // TODO: Criar Enum de StatusCode
                var error = new ApiErrorModel("Este endereço de e-mail já está cadastrado em nosso sistema!", 409, Environment.StackTrace);
                throw new ApiException(error);
            }


            //Verifico se o email ja foi cadastrado no banco de dados
            if (await _userSqlRepository.CheckIfEmailVerifyIsPendent(userEmail))
            {
                //Se não estiver cadastrado, salvo ele no banco
                await _userSqlRepository.SaveEmailToVerify(userEmail);
            }

            await SendVerificationEmail(userEmail);

        }

        public async Task<bool> CheckIfEmailIsVerified(string userEmail)
        {
            return await _userSqlRepository.CheckIfEmailIsVerified(userEmail);
        }



        private async Task SendVerificationEmail(string userEmail)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailConfig.Remetente, _emailConfig.Email));
                message.To.Add(MailboxAddress.Parse(userEmail));
                message.Subject = "Salve! Vem clicar no link!";
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"<!DOCTYPE html>
                <html lang=""en"">
                <head>
                <meta charset=""UTF-8"">
                <title>Verify Your Email</title>
                <style>
                    body {{
                        font-family: 'Arial', sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                        color: #ffffff;
                    }}
                    .email-container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background: #1a237e;
                        border: 1px solid #dedede;
                        color: #ffffff;
                    }}
                    .header {{
                        background-color: #110231;
                        color: #ffffff;
                        padding: 10px 20px;
                        text-align: center;
                    }}
                    .email-content {{
                        padding: 20px;
                    }}
                    .button {{
                        display: block;
                        width: 200px;
                        margin: 20px auto;
                        padding: 10px;
                        text-align: center;
                        background-color: #ff0000;
                        color: #ffffff;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    .footer {{
                        text-align: center;
                        padding: 10px 20px;
                        background-color: #1a237e;
                        border-top: 1px solid #dedede;
                        font-size: 12px;
                        color: #ffffff;
                    }}
                </style>
                </head>
                <body>
                    <div class=""email-container"">
                        <div class=""header"">
                            <img src=""https://schoolmatch.com.br/img/logoInteira.png"" width=""240""/>
                        </div>
                        <div class=""email-content"">
                            <h2>Salveeee,</h2>
                            <p>Para poder continuar seu cadastro, por favor clique no link abaixo para validar seu e-mail!</p>
                            <a href=""{_emailConfig.BaseUrl}{Uri.EscapeDataString(userEmail)}"" class=""button""><b>VERIFICAR</b></a>
                        </div>
                        <div class=""footer"">
                            Não esquece de seguir no insta: <a href=""https://www.instagram.com/schoolmatchfho/""><b>@schoolmatchfho</b></a>
                        </div>
                    </div>
                </body>
                </html>"
                };
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_emailConfig.SMTP, _emailConfig.Porta, true);
                await client.AuthenticateAsync(_emailConfig.Email, _emailConfig.Senha);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                throw; // Consider logging this error instead of rethrowing it, depending on your error handling policy
            }
        }

        public async Task<string> GetIdentityToken(int userId, string userMail, string userName)
        {
            using var client = new HttpClient();

            var pairs = new List<KeyValuePair<string, string>> {
                new("grant_type","external"),
                new("client_id","school_match"),
                new("client_secret","*a%ehlvfl_gvx28kp5@a8i&5zvt+a#bh$73_oimp!n!_cen0a("),
                new("scope","read write openid profile"),
                new("user_id", userId.ToString()),
                new("email", userMail),
                new("name",userName)
            };

            var content = new FormUrlEncodedContent(pairs);
            var res = await client.PostAsync(_identityUrl, content);

            if (!res.IsSuccessStatusCode)
            {
                // você pode lançar exception ou retornar null, dependendo do seu caso
                var error = await res.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao obter token: {res.StatusCode} - {error}");
            }

            var json = await res.Content.ReadAsStringAsync();

            // Desserializa direto para o DTO
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var dto = JsonSerializer.Deserialize<IdentityResponseDTO>(json, options);

            return dto.access_token;
        }

    }
}
