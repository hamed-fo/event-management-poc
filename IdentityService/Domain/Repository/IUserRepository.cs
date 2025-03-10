using IdentityService.Domain.Entity;

namespace IdentityService.Domain.Repository;

public interface IUserRepository
{
    public Task<User?> FindByNameAsync(string userName);
    
    public Task<bool> CheckPasswordAsync(User user, string password);
    
    public Task<bool> CreateUserAsync(User user, string password);
}