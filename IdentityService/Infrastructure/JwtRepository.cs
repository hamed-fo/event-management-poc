using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Domain.Entity;
using IdentityService.Domain.Repository;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure;

public class JwtRepository: IJwtRepository
{
    private IConfiguration _configuration;

    public JwtRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
        var jwtSecret = _configuration["JWT_SECRET"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var expirationHours = double.Parse(_configuration["JWT_ExpirationHours"]);
        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(expirationHours),
            signingCredentials: credentials,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}