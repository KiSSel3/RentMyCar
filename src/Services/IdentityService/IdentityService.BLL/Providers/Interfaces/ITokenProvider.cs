using System.Security.Claims;
using IdentityService.BLL.Providers.Models;
using IdentityService.Domain.Entities;

namespace IdentityService.BLL.Providers.Interfaces;

public interface ITokenProvider
{
    Task<TokenResult> GenerateAccessToken(UserEntity userEntity, CancellationToken cancellationToken = default);
    TokenResult GenerateRefreshToken();
}