using AutoMapper;
using IdentityService.BLL.Exceptions;
using IdentityService.BLL.Models.DTOs.Requests.Auth;
using IdentityService.BLL.Models.DTOs.Responses.Token;
using IdentityService.BLL.Providers.Interfaces;
using IdentityService.BLL.Services.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;

    public AuthService(UserManager<UserEntity> userManager, ITokenProvider tokenProvider, IMapper mapper, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TokensResponseDTO> LoginAsync(LoginRequestDTO loginRequestDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("User {UserName} is attempting to log in.", loginRequestDto.UserName);
        
        var user = await _userManager.FindByNameAsync(loginRequestDto.UserName) ??
                   throw new AuthorizationException("Login or password entered incorrectly.");
        
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
        if (!isPasswordCorrect)
        {
            throw new AuthorizationException("Login or password entered incorrectly.");
        }

        _logger.LogInformation("User {UserName} logged in successfully.", loginRequestDto.UserName);
        
        var tokenResponse = await GenerateTokensAsync(user, cancellationToken: cancellationToken);
        return tokenResponse;
    }

    public async Task<TokensResponseDTO> RegisterAsync(RegisterRequestDTO registerRequestDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("User {UserName} is registering.", registerRequestDto.UserName);
        
        var user = await _userManager.FindByNameAsync(registerRequestDto.UserName);
        if (user is not null)
        {
            throw new AuthorizationException("User with this UserName already exists.");
        }
        
        user = _mapper.Map<UserEntity>(registerRequestDto);
        
        await _userManager.CreateAsync(user, registerRequestDto.Password);
        await _userManager.AddToRoleAsync(user, "User");

        _logger.LogInformation("User {UserName} registered successfully.", registerRequestDto.UserName);
        
        var tokenResponse = await GenerateTokensAsync(user, cancellationToken: cancellationToken);
        return tokenResponse;
    }
    
    public async Task<TokensResponseDTO> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && !u.IsDeleted, cancellationToken) ??
                   throw new AuthorizationException("Invalid refresh token.");

        if (user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new AuthorizationException("Refresh token has expired.");
        }

        var tokenResponse = await GenerateTokensAsync(user, generateNewRefreshToken: true, cancellationToken: cancellationToken);
        return tokenResponse;
    }
    
    public async Task RevokeAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Revoking tokens for user with ID {UserId}.", userId);
        
        var user = await _userManager.FindByIdAsync(userId) ??
                   throw new EntityNotFoundException("User", userId);
        
        user.RefreshToken = string.Empty;
        user.RefreshTokenExpiryTime = DateTime.MinValue;

        await _userManager.UpdateAsync(user);
        
        _logger.LogInformation("Tokens revoked for user with ID {UserId}.", userId);
    }
    
    private async Task<TokensResponseDTO> GenerateTokensAsync(UserEntity user, bool generateNewRefreshToken = true, CancellationToken cancellationToken = default)
    {
        var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user, cancellationToken);

        if (generateNewRefreshToken)
        {
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiryTime = refreshToken.Expiration;

            await _userManager.UpdateAsync(user);
        }
        
        return new TokensResponseDTO
        {
            AccessToken = accessToken.Token,
            RefreshToken = user.RefreshToken
        };
    }
}