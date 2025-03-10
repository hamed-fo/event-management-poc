using IdentityService.Domain.Entity;

namespace IdentityService.Domain.Repository;

public interface IJwtRepository
{
    public string GenerateToken(User user);
}