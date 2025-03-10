using System.Text;
using DotNetEnv;
using IdentityService.Application;
using IdentityService.Domain.Entity;
using IdentityService.Domain.Repository;
using IdentityService.Domain.Service;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var solutionEnvPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ".env");
if (File.Exists(solutionEnvPath))
{
    Env.Load(solutionEnvPath);
}

Env.Load();
builder.Configuration.AddEnvironmentVariables();

var jwtSecret = builder.Configuration["JWT_SECRET"];
if (string.IsNullOrWhiteSpace(jwtSecret))
{
    throw new Exception("JWT_SECRET not found");
}

var key = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddSqlite<AppDbContext>(builder.Configuration["CONNECTION_STRING"])
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddOpenApi()
    .AddScoped<LoginUseCase>()
    .AddScoped<LoginService>()
    .AddScoped<JwtService>()
    .AddScoped<IJwtRepository, JwtRepository>()
    .AddScoped<RegistrationUseCase>()
    .AddScoped<RegistrationService>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Options =>
    {
        Options.RequireHttpsMetadata = false;
        Options.SaveToken = true;
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
