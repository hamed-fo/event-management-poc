using IdentityService.Domain.Entity;
using IdentityService.Domain.Service;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application;

public class RegistrationUseCase
{
    private readonly RegistrationService _registrationService;

    public RegistrationUseCase(RegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    public async Task<bool> ExecuteAsync(string username, string password, UserRole role)
    {
        return await _registrationService.RegisterUserAsync(username, password, role);
    }
}