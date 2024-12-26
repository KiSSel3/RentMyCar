using IdentityService.BLL.Models.DTOs.Requests.Role;
using IdentityService.BLL.Models.DTOs.Responses.Role;

namespace IdentityService.BLL.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleResponseDTO>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    
    Task<RoleResponseDTO> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default);
    Task<RoleResponseDTO> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken = default);

    Task CreateRoleAsync(RoleRequestDTO createRoleDTO, CancellationToken cancellationToken = default);
    Task UpdateRoleAsync(string roleId, RoleRequestDTO updateRoleDTO, CancellationToken cancellationToken = default);

    Task DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default);
}
