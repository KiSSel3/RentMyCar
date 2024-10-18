using IdentityService.BLL.Models.DTOs.Requests.Role;
using IdentityService.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Presentation.Controllers;

[ApiController]
[Route("api/role")]
[Authorize(Policy = "AdminArea")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult> GetAllRoles(CancellationToken cancellationToken = default)
    {
        var roles = await _roleService.GetAllRolesAsync(cancellationToken);
        return Ok(roles);
    }

    [HttpGet("get-by-id/{roleId}")]
    public async Task<ActionResult> GetRoleById(string roleId, CancellationToken cancellationToken = default)
    {
        var role = await _roleService.GetRoleByIdAsync(roleId, cancellationToken);
        return Ok(role);
    }

    [HttpGet("get-by-name/{roleName}")]
    public async Task<ActionResult> GetRoleByName(string roleName, CancellationToken cancellationToken = default)
    {
        var role = await _roleService.GetRoleByNameAsync(roleName, cancellationToken);
        return Ok(role);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] RoleRequestDTO createRoleDTO, CancellationToken cancellationToken = default)
    {
        await _roleService.CreateRoleAsync(createRoleDTO, cancellationToken);
        return NoContent();
    }

    [HttpPut("update/{roleId}")]
    public async Task<IActionResult> UpdateRole(string roleId, [FromBody] RoleRequestDTO updateRoleDTO, CancellationToken cancellationToken = default)
    {
        await _roleService.UpdateRoleAsync(roleId, updateRoleDTO, cancellationToken);
        return NoContent();
    }

    [HttpDelete("delete/{roleId}")]
    public async Task<IActionResult> DeleteRole(string roleId, CancellationToken cancellationToken = default)
    {
        await _roleService.DeleteRoleAsync(roleId, cancellationToken);
        return NoContent();
    }
}
