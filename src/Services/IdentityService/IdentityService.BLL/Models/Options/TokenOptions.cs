namespace IdentityService.BLL.Models.Options;

public class TokenOptions
{
    public const string DefaultSection = "Jwt";

    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
    public int AccessTokenValidityInMinutes { get; init; }
    public int RefreshTokenValidityInDays { get; init; }
}