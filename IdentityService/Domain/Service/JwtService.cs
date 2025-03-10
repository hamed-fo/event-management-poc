using IdentityService.Domain.Entity;
using IdentityService.Domain.Repository;

namespace IdentityService.Domain.Service;

public class JwtService
{
    private IJwtRepository _jwtRepository;

    public JwtService(IJwtRepository jwtRepository)
    {
        _jwtRepository = jwtRepository;
    }

    public string GenerateToken(User user)
    {
        return _jwtRepository.GenerateToken(user);
    }
}