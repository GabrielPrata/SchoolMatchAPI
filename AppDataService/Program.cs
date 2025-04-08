using AccountService.Service;
using AccountService.Services;
using AppDataService.Model;
using AppDataService.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Obter as strings de conexão do arquivo de configuração
var sqlConnection = builder.Configuration["AppConfiguration:ConnectionStringSQL"];

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
