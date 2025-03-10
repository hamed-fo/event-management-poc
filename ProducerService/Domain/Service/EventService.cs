using ProducerService.Domain.Entity;
using ProducerService.Domain.Repository;

namespace ProducerService.Domain.Service;

public class EventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Event> CreateEventAsync(Event eventEntity)
    {
        return await _eventRepository.saveAsync(eventEntity);
    }
}