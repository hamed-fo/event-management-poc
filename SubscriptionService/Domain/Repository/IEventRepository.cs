using SubscriptionService.Domain.Entity;

namespace SubscriptionService.Domain.Repository;

public interface IEventRepository
{
    public Task<List<Event>> GetAllAsync();

    public Task SaveAsync(Event newEvent);
}