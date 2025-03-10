using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application;
using SubscriptionService.Application.Dto;

namespace SubscriptionService.UserInterface.Controllers;

[Authorize(Roles = "Subscriber")]
[ApiController]
[Route("api/registrations")]
public class EventController: ControllerBase
{
    private readonly EventUseCase _eventUseCase;

    public EventController(EventUseCase eventUseCase)
    {
        _eventUseCase = eventUseCase;
    }

    [HttpGet("events")]
    public async Task<ActionResult<List<EventsResponseDto>>> GetEventsAsync()
    {
        return Ok(await _eventUseCase.GetEventsAsync());
    }
}