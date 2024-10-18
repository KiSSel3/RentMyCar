using IdentityService.BLL.Models.DTOs.Responses.User;

namespace IdentityService.BLL.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    
    Task<UserResponseDTO> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<UserResponseDTO> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
    
    Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
    
    Task AddUserToRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default);
    Task RemoveUserFromRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default);
}