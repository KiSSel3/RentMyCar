using IdentityService.BLL.Models.Results;
using IdentityService.Domain.Entities;

namespace IdentityService.BLL.Providers.Interfaces;

public interface ITokenProvider
{
    Task<TokenResult> GenerateAccessToken(UserEntity userEntity, CancellationToken cancellationToken = default);
    TokenResult GenerateRefreshToken();
}