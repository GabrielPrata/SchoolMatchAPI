using MatchService.Service;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var sqlConnection = builder.Configuration["AppConfiguration:ConnectionStringSQL"];

builder.Services.AddSingleton<SqlConnection>(provider =>
{
    return new SqlConnection(sqlConnection);
});

builder.Services.AddScoped<IMatchService>(provider =>
{
    var sqlConnection = provider.GetRequiredService<SqlConnection>();
    return new MatchServiceApp(sqlConnection);
});

builder.Services.AddControllers();

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
