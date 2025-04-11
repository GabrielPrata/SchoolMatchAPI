using AccountService.Service;
using AccountService.Services;
using AppDataService.Model;
using AppDataService.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Obter as strings de conexão do arquivo de configuração
var sqlConnection = builder.Configuration["AppConfiguration:ConnectionStringSQL"];

builder.Services.Configure<BasicAuthSettings>(
    builder.Configuration.GetSection("BasicAuthSettings"));



// Add services to the container.
//passar a instancia da classe de conexao aqui
builder.Services.AddSingleton<SqlConnection>(provider =>
{
    return new SqlConnection(sqlConnection);
});


builder.Services.AddScoped<ICourseDataService>(provider =>
{
    var sqlConnection = provider.GetRequiredService<SqlConnection>();
    return new CourseDataService(sqlConnection);
});

builder.Services.AddScoped<IBlockDataService>(provider =>
{
    var sqlConnection = provider.GetRequiredService<SqlConnection>();
    return new BlockDataService(sqlConnection);
});

builder.Services.AddScoped<ISexualityDataService>(provider =>
{
    var sqlConnection = provider.GetRequiredService<SqlConnection>();
    return new SexualityDataService(sqlConnection);
});

builder.Services.AddScoped<IInterestsDataService>(provider =>
{
    var sqlConnection = provider.GetRequiredService<SqlConnection>();
    return new InterestsDataService(sqlConnection);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "App Data Service", Version = "v1" });

    // Adiciona a definição de segurança
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Insira seu nome de usuário e senha no formato: user:password (será codificado automaticamente)"
    });

    // Exige autenticação basic para todos os endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddAuthorization();

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
