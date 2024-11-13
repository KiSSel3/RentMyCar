using AutoMapper;
using IdentityService.BLL.Exceptions;
using IdentityService.BLL.Models.DTOs.Requests.Role;
using IdentityService.BLL.Models.DTOs.Responses.Role;
using IdentityService.BLL.Services.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.Services.Implementation;

public class RoleService : IRoleService
{
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly ILogger<RoleService> _logger;
    private readonly IMapper _mapper;
    
    public RoleService(
        RoleManager<RoleEntity> roleManager,
        ILogger<RoleService> logger,
        IMapper mapper)
    {
        _roleManager = roleManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleResponseDTO>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all roles.");
        
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        
        _logger.LogInformation("Fetched {RoleCount} roles.", roles.Count);
        
        return _mapper.Map<IEnumerable<RoleResponseDTO>>(roles);
    }

    public async Task<RoleResponseDTO> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching role by ID: {RoleId}", roleId);

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            throw new EntityNotFoundException("Role", roleId);
        }
        
        _logger.LogInformation("Fetched role: {RoleName} with ID: {RoleId}", role.Name, roleId);
        
        return _mapper.Map<RoleResponseDTO>(role);
    }

    public async Task<RoleResponseDTO> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching role by name: {RoleName}", roleName);

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role is null)
        {
            throw new EntityNotFoundException($"Role with name {roleName} not found");
        }
        
        _logger.LogInformation("Fetched role: {RoleName} with ID: {RoleId}", role.Name, role.Id);
        
        return _mapper.Map<RoleResponseDTO>(role);
    }

    public async Task CreateRoleAsync(RoleRequestDTO createRoleDTO, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating role with name: {RoleName}", createRoleDTO.Name);
        
        var role = await _roleManager.FindByNameAsync(createRoleDTO.Name);
        if (role is not null)
        {
            throw new EntityAlreadyExistsException($"Role with name '{createRoleDTO.Name}' already exists.");
        }

        role = _mapper.Map<RoleEntity>(createRoleDTO);
        await _roleManager.CreateAsync(role);
        
        _logger.LogInformation("Role '{RoleName}' created successfully.", createRoleDTO.Name);
    }

    public async Task UpdateRoleAsync(string roleId, RoleRequestDTO updateRoleDTO, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating role with ID: {RoleId}", roleId);

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            throw new EntityNotFoundException("Role", roleId);
        }
        
        var existingRole = await _roleManager.FindByNameAsync(updateRoleDTO.Name);
        if (existingRole is not null && existingRole.Id != role.Id)
        {
            throw new EntityAlreadyExistsException($"Role with name '{updateRoleDTO.Name}' already exists.");
        }

        _mapper.Map(updateRoleDTO, role);
        await _roleManager.UpdateAsync(role);
        
        _logger.LogInformation("Role with ID: {RoleId} updated successfully.", roleId);
    }

    public async Task DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting role with ID: {RoleId}", roleId);
        
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            throw new EntityNotFoundException("Role", roleId);
        }

        await _roleManager.DeleteAsync(role);
        
        _logger.LogInformation("Role with ID: {RoleId} deleted successfully.", roleId);
    }
}