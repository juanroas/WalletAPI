using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WalletAPI.Application.Services.Implementations;
using WalletAPI.Application.Services.Interfaces;
using WalletAPI.Domain.Models;
using WalletAPI.Infrastructure.Data;
using WalletAPI.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adicionando suporte ao banco de dados PostgreSQL
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var config = builder.Configuration;

// Conexões de banco e Redis
var connectionString = config.GetSection("ConnectionStrings:DefaultConnection").Value;
var redisConnection = config.GetSection("ConnectionStrings:Redis").Value;

// Configuracao do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// identidade
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adicionando os servicos da camada de infraestrutura (inclui Redis e Kafka)
builder.Services.AddInfrastructureServices(redisConnection, builder.Configuration);


// Adicionando servicos da aplicacao
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();


//JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

//  Configuracao do Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Wallet API", Version = "v1" });
});

var app = builder.Build();

// Log de inicializacao
app.Logger.LogInformation("API rodando em: {0}", builder.Configuration["ASPNETCORE_URLS"]);

// Configuracao do ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallet API v1"));
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.RoutePrefix = "swagger");
}

// Middlewares essenciais
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
app.UseHttpsRedirection();


app.Run();