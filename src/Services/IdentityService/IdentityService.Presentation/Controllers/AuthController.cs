using IdentityService.BLL.Models.DTOs.Requests.Auth;
using IdentityService.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO loginRequestDto, CancellationToken cancellationToken = default)
    {
        var tokenResponse = await _authService.LoginAsync(loginRequestDto, cancellationToken);
        return Ok(tokenResponse);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDTO registerRequestDto, CancellationToken cancellationToken = default)
    {
        var tokenResponse = await _authService.RegisterAsync(registerRequestDto, cancellationToken);
        return Ok(tokenResponse);
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenResponse = await _authService.RefreshTokenAsync(refreshToken, cancellationToken);
        return Ok(tokenResponse);
    }
    
    [Authorize]
    [HttpDelete("logout/{userId}")]
    public async Task<IActionResult> LogoutAsync(string userId, CancellationToken cancellationToken = default)
    {
        await _authService.RevokeAsync(userId, cancellationToken);
        return Ok();
    }
}