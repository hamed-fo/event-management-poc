using IdentityService.Application;
using IdentityService.Controller.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controller;

[ApiController]
[Route("api/auth/login")]
public class LoginController: ControllerBase
{
    private readonly LoginUseCase _useCase;

    public LoginController(LoginUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var token = await _useCase.ExecuteAsync(request.username, request.password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid credentials!" });
        }
    }
}