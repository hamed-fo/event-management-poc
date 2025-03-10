using IdentityService.Domain.Service;

namespace IdentityService.Application;

public class LoginUseCase
{
    private readonly LoginService _loginService;
    private readonly JwtService _jwtService;

    public LoginUseCase(LoginService loginService, JwtService jwtService)
    {
        _loginService = loginService;
        _jwtService = jwtService;
    }

    public async Task<string> ExecuteAsync(string username, string password)
    {
        var user = await _loginService.AuthenticateUserAsync(username, password);
        return _jwtService.GenerateToken(user);
    }
}