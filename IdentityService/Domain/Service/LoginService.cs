using IdentityService.Domain.Entity;
using IdentityService.Domain.Repository;

namespace IdentityService.Domain.Service;

public class LoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AuthenticateUserAsync(string username, string password)
    {
        var user = await _userRepository.FindByNameAsync(username);
        if (user == null || !await _userRepository.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedAccessException("Invalid credentials!");
        }

        return user;
    }
}