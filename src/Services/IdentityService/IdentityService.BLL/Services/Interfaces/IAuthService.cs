using IdentityService.BLL.Models.DTOs.Requests.Auth;
using IdentityService.BLL.Models.DTOs.Responses.Token;

namespace IdentityService.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<TokensResponseDTO> LoginAsync(LoginRequestDTO loginRequestDto, CancellationToken cancellationToken = default);
    Task<TokensResponseDTO> RegisterAsync(RegisterRequestDTO registerRequestDto, CancellationToken cancellationToken = default);
    Task<TokensResponseDTO> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeAsync(Guid userId, CancellationToken cancellationToken = default);
}