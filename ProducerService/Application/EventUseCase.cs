using ProducerService.Application.Dto;
using ProducerService.Controller.Dto;
using ProducerService.Domain.Entity;
using ProducerService.Domain.Service;

namespace ProducerService.Application;

public class EventUseCase
{
    private readonly EventService _eventService;

    public EventUseCase(EventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<NewEventResponseDto> CreateEventAsync(NewEventRequestDto newEvent, string userId)
    {
        var eventEntity = new Event
        {
            ProducerId = userId,
            Name = newEvent.Name,
            Description = newEvent.Description,
            Location = newEvent.Location,
            StartTime = newEvent.StartTime,
            EndTime = newEvent.EndTime,
        };

        var newEventEntity = await _eventService.CreateEventAsync(eventEntity);

        return new NewEventResponseDto(
            newEventEntity.Id,
            newEventEntity.ProducerId,
            newEventEntity.Name,
            newEventEntity.Description,
            newEventEntity.Location,
            newEventEntity.StartTime,
            newEventEntity.EndTime);
    }
}