using AccountService.Service;
using AccountService.Data.DTO;
using AccountService.Repository;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using AccountService.Model.Base;

namespace AccountService.Services;

public class UserDataService : IUserDataService
{
    private readonly UserRepository _userRepository;
    private readonly EmailConfig _emailConfig;

    public UserDataService(string sqlConnection, string mongoConnection, IOptions<EmailConfig> emailConfig)
    {
        _userRepository = new UserRepository(sqlConnection, mongoConnection);
        _emailConfig = emailConfig.Value;
    }

    public async Task<UserDataDTO> GetUserDataById(int userId)
    {
        var data = await _userRepository.GetUserDataById(userId);
        return data;
    }

    public async Task SaveUserData(UserDataDTO userData)
    {
        await _userRepository.SaveUserData(userData);
    }

    public async Task UpdateUserData(UserDataDTO userData)
    {
        //TODO: FINALIZAR A IMPLEMENTACAO CORRETA DO MÉTODO
        await _userRepository.UpdateUserData(userData);
    }

    public async Task SaveEmailToVerify(string userEmail)
    {
        if (await _userRepository.SaveEmailToVerify(userEmail))
        {
            SendVerificationEmail(userEmail);
        }

    }

    public async Task<bool> CheckIfEmailIsVerified(string userEmail)
    {
       return await _userRepository.CheckIfEmailIsVerified(userEmail);
    }

    public async Task SendVerificationEmail(string userEmail)
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
}
