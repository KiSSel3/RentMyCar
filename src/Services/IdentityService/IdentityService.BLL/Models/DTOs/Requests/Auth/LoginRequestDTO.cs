namespace IdentityService.BLL.Models.DTOs.Requests.Auth;

public class LoginRequestDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
}