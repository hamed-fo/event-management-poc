using IdentityService.Domain.Entity;
using IdentityService.Domain.Repository;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> CreateUserAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded == false)
        {
            return false;
        }
        
        await _userManager.AddToRoleAsync(user, user.Role.ToString());
        return true;
    }
}