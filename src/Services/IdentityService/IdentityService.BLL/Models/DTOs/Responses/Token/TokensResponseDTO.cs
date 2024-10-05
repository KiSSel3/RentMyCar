namespace IdentityService.BLL.Models.DTOs.Responses.Token;

public class TokensResponseDTO
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}