using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var solutionEnvPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ".env");
if (File.Exists(solutionEnvPath))
{
    Env.Load(solutionEnvPath);
}

builder.Configuration.AddEnvironmentVariables();

// changing the host name for local and docker
var ocelotJson = File.ReadAllText("ocelot.template.json");
ocelotJson = ocelotJson.Replace("{IDENTITY_HOST}", Environment.GetEnvironmentVariable("IDENTITY_HOST") ?? "localhost")
    .Replace("{PRODUCER_HOST}", Environment.GetEnvironmentVariable("PRODUCER_HOST") ?? "localhost")
    .Replace("{SUBSCRIPTION_HOST}", Environment.GetEnvironmentVariable("SUBSCRIPTION_HOST") ?? "localhost");
var tempOcelotFile = "ocelot-temp.json";
File.WriteAllText(tempOcelotFile, ocelotJson);

builder.Configuration.AddJsonFile(tempOcelotFile, optional: false, reloadOnChange: true);

var jwtSecret = builder.Configuration["JWT_SECRET"];
if (string.IsNullOrWhiteSpace(jwtSecret))
{
    throw new Exception("JWT_SECRET not found");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddOcelot();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();

app.Run();