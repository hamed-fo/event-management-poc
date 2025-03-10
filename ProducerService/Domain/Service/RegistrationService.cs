using Microsoft.EntityFrameworkCore;
using ProducerService.Domain.Entity;
using ProducerService.Domain.Repository;

namespace ProducerService.Domain.Service;

public class RegistrationService
{
    private IRegistrationRepository _registrationRepository;

    public RegistrationService(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }
    
    public async Task<List<Registration>> GetRegistrationsAsync(int eventId)
    {
        return await _registrationRepository.GetRegistrationsAsync(eventId);
    }

    public async Task SaveRegistrationAsync(Registration registration)
    {
        await _registrationRepository.SaveRegistrationAsync(registration);
    }
}