using AccountService.Data.DTO;
using AccountService.Repository.Queries;
using AccountService.Mappers;
using AccountService.Model.MongoModels;
using AccountService.Model.SqlModels;
using AccountService.Model.Base;

namespace AccountService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataSqlQueries _sqlQueries;
        private readonly UserDataMongoQueries _mongoQueries;

        public UserRepository(string sqlConnection, string mongoConnection)
        {
            _sqlQueries = new UserDataSqlQueries(sqlConnection);
            _mongoQueries = new UserDataMongoQueries(mongoConnection);

        }
        public async Task<UserDataDTO> GetUserDataById(int userId)
        {
            try
            {
                #region Recuperando os dados do SQL
                var sqlModel = await _sqlQueries.GetUserDataById(userId);
                var mongoModel = await _mongoQueries.GetUserById(userId);



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

        public async Task SaveUserData(UserDataDTO userData)
        {
            // TODO: separar a regra de negócio do repository:
            // TODO: Aplicar unitOfWork - Commit Transactions, caso alguma dessas operações de erro, execute um rollback.
            if (await _sqlQueries.VerifyUserExist(userData.EmailUsuario))
            {
                var error = new ApiErrorModel("Este endereço de e-mail já está cadastrado em nosso sistema!", 409, Environment.StackTrace);
                throw new ApiException(error);
            }


            SqlUserData sqlData = userData.ToSqlModel();
            MongoUserData mongoData = userData.ToMongoModel();

            int userId = await _sqlQueries.SaveUserData(sqlData);

            foreach (int genreId in sqlData.UsuarioPreferenciaGenero)
            {
                await _sqlQueries.SaveUserGenreInterests(userId, genreId);
            }

            sqlData.BlocosUsario.Add(sqlData.BlocoPrincipalId);
            foreach (int blockId in sqlData.BlocosUsario)
            {
                await _sqlQueries.SaveUserBlocks(userId, blockId);
            }
            mongoData.IdUsuario = userId;
            await _mongoQueries.SaveUserData(mongoData);
        }

        public async Task<bool> SaveEmailToVerify(string userEmail)
        {
            //TODO: Criar classe static paa retornar as mensagens, Ex: ConstanteMensagens.EmailNaoCadastrado
            if (await _sqlQueries.CheckIfEmailIsVerified(userEmail))
            {
                // TODO: Criar Enum de StatusCode
                var error = new ApiErrorModel("Este endereço de e-mail já está cadastrado em nosso sistema!", 409, Environment.StackTrace);
                throw new ApiException(error);
            }


            //Verifico se o email ja foi cadastrado no banco de dados
            if (await _sqlQueries.CheckIfEmailVerifyIsPendent(userEmail))
            {
                //Se não estiver cadastrado, salvo ele no banco
                await _sqlQueries.SaveEmailToVerify(userEmail);
            }

            return true;
            
        }

        public async Task<bool> CheckIfEmailIsVerified(string userEmail)
        {
            return await _sqlQueries.CheckIfEmailIsVerified(userEmail);
        }
    }
}
