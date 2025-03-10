using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SubscriptionService.Application;
using SubscriptionService.Domain.Repository;
using SubscriptionService.Domain.Service;
using SubscriptionService.Infrastructure;
using SubscriptionService.Infrastructure.Mapper;
using SubscriptionService.UserInterface.Messaging;

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

builder.Services.AddOpenApi()
    .AddScoped<EventUseCase>()
    .AddScoped<EventService>()
    .AddScoped<IEventRepository, EventRepository>()
    .AddScoped<RegistrationMapper>()
    .AddScoped<RegistrationUseCase>()
    .AddScoped<RegistrationService>()
    .AddScoped<IRegistrationRepository, RegistrationRepository>()
    .AddSingleton<RabbitMqPublisher>()
    .AddSingleton<RabbitMqConsumer>()
    .AddControllers();

builder.Services.AddSqlite<RegistrationDbContext>(builder.Configuration["CONNECTION_STRING"]);

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
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

var consumer = app.Services.GetRequiredService<RabbitMqConsumer>();
await consumer.StartListeningAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();