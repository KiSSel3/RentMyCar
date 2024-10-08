using AutoMapper;
using IdentityService.BLL.Exceptions;
using IdentityService.BLL.Models.DTOs.Responses.User;
using IdentityService.BLL.Services.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.Services.Implementation;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    
    public UserService(
        UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager,
        ILogger<UserService> logger,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all users.");
        
        var users = await _userManager.Users.ToListAsync(cancellationToken);
        
        _logger.LogInformation("Fetched {UserCount} users.", users.Count);
        
        return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
    }

    public async Task<UserResponseDTO> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Fetching user with ID: {userId}.");

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException("User", userId);
        }

        _logger.LogInformation("Fetched user: {UserName} with ID: {UserId}", user.UserName, userId);
        
        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Fetching user with name: {username}.");

        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
        {
            throw new EntityNotFoundException($"User with username {username} not found");
        }

        _logger.LogInformation("Fetched user: {UserName} with ID: {UserId}", user.UserName, user.Id);
        
        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Deleting user with ID: {userId}.");
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException("User", userId);
        }
        
        await _userManager.DeleteAsync(user);
        
        _logger.LogInformation($"User with ID: {userId} deleted successfully.");
    }

    public async Task AddUserToRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Adding user with ID: {userId} to role: {roleName}.");
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException("User", userId);
        }

        var isExist = await _roleManager.RoleExistsAsync(roleName);
        if (!isExist)
        {
            throw new EntityNotFoundException($"Role {roleName} does not exist");
        }

        await _userManager.AddToRoleAsync(user, roleName);
        
        _logger.LogInformation($"User with ID: {userId} added to role: {roleName}.");
    }

    public async Task RemoveUserFromRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Removing user with ID: {userId} from role: {roleName}.");

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException("User", userId);
        }

        var isExist = await _roleManager.RoleExistsAsync(roleName);
        if (!isExist)
        {
            throw new EntityNotFoundException($"Role {roleName} does not exist");
        }
        
        await _userManager.RemoveFromRoleAsync(user, roleName);
        
        _logger.LogInformation($"User with ID: {userId} removed from role: {roleName}.");
    }
}