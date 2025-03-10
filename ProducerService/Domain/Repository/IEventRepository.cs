using ProducerService.Domain.Entity;

namespace ProducerService.Domain.Repository;

public interface IEventRepository
{
    public Task<Event> saveAsync(Event eventEntity);
}