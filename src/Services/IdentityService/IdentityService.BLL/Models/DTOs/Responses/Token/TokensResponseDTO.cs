namespace IdentityService.BLL.Models.DTOs.Responses.Token;

public class TokensResponseDTO
{
    public string JwtToken { get; set; }

    public string RefreshToken { get; set; }
}