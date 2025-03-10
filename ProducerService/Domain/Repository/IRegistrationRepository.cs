using ProducerService.Domain.Entity;

namespace ProducerService.Domain.Repository;

public interface IRegistrationRepository
{
    public Task<List<Registration>> GetRegistrationsAsync(int eventId);
    
    public Task SaveRegistrationAsync(Registration registration);
}