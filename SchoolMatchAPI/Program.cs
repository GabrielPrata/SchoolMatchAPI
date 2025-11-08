using AccountService.Model.Base;
using AccountService.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AccountService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Obter as strings de conexão do arquivo de configuração
            var sqlConnection = builder.Configuration["AppConfiguration:ConnectionStringSQL"];
            var mongoConnection = builder.Configuration["AppConfiguration:ConnectionStringMongo"];
            var identityUrl = builder.Configuration["AppConfiguration:IdentityUrl"];

            // Add services to the container.
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailNaoResponda"));
            builder.Services.AddScoped<IUserDataService>(provider =>
            {
                var emailConfig = provider.GetRequiredService<IOptions<EmailConfig>>();
                return new UserDataService(sqlConnection, mongoConnection, emailConfig, identityUrl);
            });


            builder.Services.AddControllers();

            //Configuração da autenticação com o Identity Service
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                //AQUI VAI O ENDEREÇO DO IDENTITY SERVICE
                options.Authority = builder.Configuration["IdentitySettings:ServiceUrl"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };

            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "schoolMatch");
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Insira 'Bearer' [space] e seu token!",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                        },
                     new List<string>()
                    }
                });
            });

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