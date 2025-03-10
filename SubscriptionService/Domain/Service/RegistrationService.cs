using SubscriptionService.Domain.Entity;
using SubscriptionService.Domain.Repository;

namespace SubscriptionService.Domain.Service;

public class RegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;

    public RegistrationService(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }
    
    public async Task<Registration> RegisterAsync(Registration registration)
    {
        return await _registrationRepository.SaveAsync(registration);
    }

}