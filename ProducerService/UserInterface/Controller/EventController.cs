using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProducerService.Application;
using ProducerService.Controller.Dto;

namespace ProducerService.UserInterface.Controller;

[Authorize(Roles = "Producer")]
[ApiController]
[Route("api/events")]
public class EventController: ControllerBase
{
    private EventUseCase _eventUseCase;

    public EventController(EventUseCase eventUseCase)
    {
        _eventUseCase = eventUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] NewEventRequestDto newEvent)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid token!" });
        }

        var eventResponseDto = await _eventUseCase.CreateEventAsync(newEvent, userId);
        return Created(string.Empty, eventResponseDto);
    }
}