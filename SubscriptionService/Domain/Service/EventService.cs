using SubscriptionService.Domain.Entity;
using SubscriptionService.Domain.Repository;

namespace SubscriptionService.Domain.Service;

public class EventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _eventRepository.GetAllAsync();
    }
    
    public async Task SaveEventAsync(Event newEvent)
    {
        await _eventRepository.SaveAsync(newEvent);
    }
}