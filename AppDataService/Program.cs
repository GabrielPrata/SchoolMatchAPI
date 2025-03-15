using AccountService.Service;
using AccountService.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Obter as strings de conexão do arquivo de configuração
var sqlConnection = builder.Configuration["AppConfiguration:ConnectionStringSQL"];

// Add services to the container.
builder.Services.AddScoped<ICourseDataService>(provider =>
{
    return new CourseDataService(sqlConnection);
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
