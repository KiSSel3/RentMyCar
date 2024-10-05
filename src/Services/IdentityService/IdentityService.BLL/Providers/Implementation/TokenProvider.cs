using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IdentityService.BLL.Models.Results;
using IdentityService.BLL.Providers.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TokenOptions = IdentityService.BLL.Models.Options.TokenOptions;

namespace IdentityService.BLL.Providers.Implementation;

public class TokenProvider : ITokenProvider
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly TokenOptions _tokenOptions;

    public TokenProvider(IOptions<TokenOptions> tokenOptions, UserManager<UserEntity> userManager)
    {
        _tokenOptions = tokenOptions.Value;
        _userManager = userManager;
    }

    public async Task<TokenResult> GenerateAccessToken(UserEntity userEntity, CancellationToken cancellationToken = default)
    {
        var claims = await GetClaimsAsync(userEntity);
        
        var signingCredentials = GetSigningCredentials();
        var tokenResult = GenerateToken(signingCredentials, claims);
        
        return tokenResult;
    }

    public TokenResult GenerateRefreshToken()
    {
        int size = 64;
        var randomNumber = new byte[size];
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        var tokenResult = new TokenResult()
        {
            Token = Convert.ToBase64String(randomNumber),
            Expiration = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenValidityInDays)
        };

        return tokenResult;
    }
    
    private async Task<List<Claim>> GetClaimsAsync(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName)
        };
        
        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_tokenOptions.SecretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private TokenResult GenerateToken(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var expires = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenValidityInMinutes);
        
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);;

        var tokenResult = new TokenResult()
        {
            Token = accessToken,
            Expiration = expires
        };
        
        return tokenResult;
    }
}