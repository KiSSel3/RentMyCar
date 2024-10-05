namespace IdentityService.BLL.Providers.Models;

public class TokenResult
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}