using SubscriptionService.Application.Dto;
using SubscriptionService.Domain.Entity;
using SubscriptionService.Domain.Service;
using SubscriptionService.UserInterface.Dto;

namespace SubscriptionService.Application;

public class EventUseCase
{
    private readonly EventService _eventService;

    public EventUseCase(EventService eventService)
    {
        _eventService = eventService;
    }
    
    public async Task<List<EventsResponseDto>> GetEventsAsync()
    {
        var events = await _eventService.GetAllAsync();

        return events.Select(e => new EventsResponseDto(
            e.Id,
            e.Name,
            e.Description,
            e.Location,
            e.StartTime,
            e.EndTime)).ToList();
    }

    public async Task SaveEventAsync(NewEventDto eventDto)
    {
        var newEvent = new Event
        {
            Id = eventDto.Id,
            Name = eventDto.Name,
            Description = eventDto.Description,
            Location = eventDto.Location,
            StartTime = eventDto.StartTime,
            EndTime = eventDto.EndTime
        };

        await _eventService.SaveEventAsync(newEvent);
    }
}