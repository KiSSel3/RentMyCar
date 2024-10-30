using AutoMapper;
using IdentityService.BLL.Exceptions;
using IdentityService.BLL.Models.DTOs.Requests.Auth;
using IdentityService.BLL.Models.DTOs.Responses.Token;
using IdentityService.BLL.Providers.Interfaces;
using IdentityService.BLL.Publishers.Interfaces;
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
    private readonly INotificationPublisher _notificationPublisher;
    
    public AuthService(
        UserManager<UserEntity> userManager,
        ITokenProvider tokenProvider,
        ILogger<AuthService> logger,
        IMapper mapper,
        INotificationPublisher notificationPublisher)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
        _mapper = mapper;
        _notificationPublisher = notificationPublisher;
    }

    public async Task<TokensResponseDTO> LoginAsync(LoginRequestDTO loginRequestDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("User {UserName} is attempting to log in.", loginRequestDto.UserName);

        var user = await _userManager.FindByNameAsync(loginRequestDto.UserName);
        if (user is null)
        {
            throw new AuthorizationException("Login or password entered incorrectly.");
        }
        
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

        var user = _mapper.Map<UserEntity>(registerRequestDto);
        
        var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException("A user with this data already exists.");
        }
        
        await _userManager.AddToRoleAsync(user, "User");

        _logger.LogInformation("User {UserName} registered successfully.", registerRequestDto.UserName);

        await _notificationPublisher.PublishUserRegisteredMessage(user, cancellationToken);
        
        var tokenResponse = await GenerateTokensAsync(user, cancellationToken: cancellationToken);
        return tokenResponse;
    }
    
    public async Task<TokensResponseDTO> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && !u.IsDeleted,
            cancellationToken);
        if (user is null)
        {
            throw new AuthorizationException("Invalid refresh token.");
        }
        
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

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException("User", userId);
        }
        
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