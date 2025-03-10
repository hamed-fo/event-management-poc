using IdentityService.Domain.Entity;
using IdentityService.Domain.Repository;

namespace IdentityService.Domain.Service;

public class RegistrationService
{
    private readonly IUserRepository _userRepository;

    public RegistrationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> RegisterUserAsync(string username, string password, UserRole role)
    {
        var user = new User
        {
            UserName = username,
            Role = role
        };

        return await _userRepository.CreateUserAsync(user, password);
    }
}