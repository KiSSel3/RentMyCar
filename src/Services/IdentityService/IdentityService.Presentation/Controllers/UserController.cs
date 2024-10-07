using IdentityService.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Presentation.Controllers;

[ApiController]
[Route("api/user")]
[Authorize(Policy = "AdminArea")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        return Ok(users);
    }
    
    [HttpGet("get-by-id{userId}")]
    public async Task<ActionResult> GetUserById(string userId, CancellationToken cancellationToken  = default)
    {
        var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
        return Ok(user);
    }

    [HttpGet("get-by-username/{username}")]
    public async Task<ActionResult> GetUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetUserByUsernameAsync(username, cancellationToken);
        return Ok(user);
    }

    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId, CancellationToken cancellationToken = default)
    {
        await _userService.DeleteUserAsync(userId, cancellationToken);
        return NoContent();
    }

    [HttpPost("add-user-to-role/{userId}")]
    public async Task<IActionResult> AddUserToRole(string userId, [FromBody] string roleName, CancellationToken cancellationToken = default)
    {
        await _userService.AddUserToRoleAsync(userId, roleName, cancellationToken);
        return NoContent();
    }

    [HttpDelete("remove-user-from-role/{userId}")]
    public async Task<IActionResult> RemoveUserFromRole(string userId, [FromBody] string roleName, CancellationToken cancellationToken = default)
    {
        await _userService.RemoveUserFromRoleAsync(userId, roleName, cancellationToken);
        return NoContent();
    }
}