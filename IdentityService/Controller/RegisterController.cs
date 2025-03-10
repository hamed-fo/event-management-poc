using IdentityService.Application;
using IdentityService.Controller.Dto;
using IdentityService.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controller;

[ApiController]
[Route("api/auth/register")]
public class RegisterController: ControllerBase
{
    private readonly RegistrationUseCase _useCase;

    public RegisterController(RegistrationUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        if (!Enum.TryParse(requestDto.role, out UserRole role))
        {
            return BadRequest(new { message = "Invalid role!" });
        }

        var result = await _useCase.ExecuteAsync(requestDto.username, requestDto.password, role);
        return result ? 
            Ok(new { message = "User registered successfully." }) : 
            BadRequest(new { message = "Bad request!" });
    }
}