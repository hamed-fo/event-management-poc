using SubscriptionService.Domain.Entity;

namespace SubscriptionService.Domain.Repository;

public interface IRegistrationRepository
{
    public Task<Registration> SaveAsync(Registration registration);
}