using AutoMapper;
using IdentityService.BLL.Exceptions;
using IdentityService.BLL.Models.DTOs.Responses.User;
using IdentityService.BLL.Services.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.BLL.Services.Implementation;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
    }

    public async Task<UserResponseDTO> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
                   throw new EntityNotFoundException("User", userId);

        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(username) ??
                   throw new EntityNotFoundException($"User with username {username} not found");

        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
                   throw new EntityNotFoundException("User", userId);
        
        await _userManager.DeleteAsync(user);
    }

    public async Task AddUserToRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
                   throw new EntityNotFoundException("User", userId);

        var isExist = await _roleManager.RoleExistsAsync(roleName);
        if (!isExist)
        {
            throw new EntityNotFoundException($"Role {roleName} does not exist");
        }

        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task RemoveUserFromRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
                   throw new EntityNotFoundException("User", userId);

        var isExist = await _roleManager.RoleExistsAsync(roleName);
        if (!isExist)
        {
            throw new EntityNotFoundException($"Role {roleName} does not exist");
        }
        
        await _userManager.RemoveFromRoleAsync(user, roleName);
    }
}