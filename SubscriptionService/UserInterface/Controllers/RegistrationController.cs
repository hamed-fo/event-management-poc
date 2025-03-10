using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application;
using SubscriptionService.UserInterface.Dto;

namespace SubscriptionService.UserInterface.Controllers;

[Authorize(Roles = "Subscriber")]
[ApiController]
[Route("api/registrations")]
public class RegistrationController: ControllerBase
{
    private readonly RegistrationUseCase _registrationUseCase;

    public RegistrationController(RegistrationUseCase registrationUseCase)
    {
        _registrationUseCase = registrationUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> NewRegistration([FromBody] NewRegistrationRequestDto registrationRequest)
    {
        var registrationEntity = await _registrationUseCase.NewRegistrationAsync(registrationRequest);
        return Created(string.Empty, registrationEntity);
    }
}