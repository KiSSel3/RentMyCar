using IdentityService.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Presentation.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Policy = "AdminArea")]
    [HttpGet("get-all")]
    public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("get-by-id/{userId}")]
    public async Task<ActionResult> GetUserById(string userId, CancellationToken cancellationToken  = default)
    {
        var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
        return Ok(user);
    }

    [Authorize]
    [HttpGet("get-by-username/{username}")]
    public async Task<ActionResult> GetUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetUserByUsernameAsync(username, cancellationToken);
        return Ok(user);
    }

    [Authorize(Policy = "AdminArea")]
    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId, CancellationToken cancellationToken = default)
    {
        await _userService.DeleteUserAsync(userId, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = "AdminArea")]
    [HttpPost("add-user-to-role/{userId}")]
    public async Task<IActionResult> AddUserToRole(string userId, [FromBody] string roleName, CancellationToken cancellationToken = default)
    {
        await _userService.AddUserToRoleAsync(userId, roleName, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = "AdminArea")]
    [HttpDelete("remove-user-from-role/{userId}")]
    public async Task<IActionResult> RemoveUserFromRole(string userId, [FromBody] string roleName, CancellationToken cancellationToken = default)
    {
        await _userService.RemoveUserFromRoleAsync(userId, roleName, cancellationToken);
        return NoContent();
    }
}