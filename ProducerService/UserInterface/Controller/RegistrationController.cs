using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProducerService.Application;
using ProducerService.Application.Dto;

namespace ProducerService.UserInterface.Controller;

[Authorize(Roles = "Producer")]
[ApiController]
[Route("api/events")]
public class RegistrationController: ControllerBase
{
    private RegistrationUseCase _registrationUseCase;

    public RegistrationController(RegistrationUseCase registrationUseCase)
    {
        _registrationUseCase = registrationUseCase;
    }
    
    [HttpGet("{eventId}/registrations")]
    public async Task<ActionResult<List<RegistrationsResponseDto>>> GetEventRegistrations(int eventId)
    {
        var registrations = await _registrationUseCase.GetRegistrationsAsync(eventId);
        return Ok(registrations);
    }
}