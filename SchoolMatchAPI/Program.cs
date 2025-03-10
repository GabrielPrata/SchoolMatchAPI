using AccountService.Model.Base;
using AccountService.Service;
using AccountService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AccountService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Obter as strings de conex�o do arquivo de configura��o
            var sqlConnection = builder.Configuration["AppConfiguration:ConnectionStringSQL"];
            var mongoConnection = builder.Configuration["AppConfiguration:ConnectionStringMongo"];

            // Add services to the container.
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailNaoResponda"));
            builder.Services.AddScoped<IUserDataService>(provider =>
            {
                var emailConfig = provider.GetRequiredService<IOptions<EmailConfig>>();
                return new UserDataService(sqlConnection, mongoConnection, emailConfig);
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}