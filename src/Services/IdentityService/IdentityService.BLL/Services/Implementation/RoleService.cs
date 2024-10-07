using AutoMapper;
using IdentityService.BLL.Exceptions;
using IdentityService.BLL.Models.DTOs.Requests.Role;
using IdentityService.BLL.Models.DTOs.Responses.Role;
using IdentityService.BLL.Services.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.BLL.Services.Implementation;

public class RoleService : IRoleService
{
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<RoleEntity> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleResponseDTO>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<RoleResponseDTO>>(roles);
    }

    public async Task<RoleResponseDTO> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        return _mapper.Map<RoleResponseDTO>(role);
    }

    public async Task<RoleResponseDTO> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        return _mapper.Map<RoleResponseDTO>(role);
    }

    public async Task CreateRoleAsync(RoleRequestDTO createRoleDTO, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByNameAsync(createRoleDTO.Name);
        if (role is not null)
        {
            throw new EntityAlreadyExistsException($"Role with name '{createRoleDTO.Name}' already exists.");
        }

        role = _mapper.Map<RoleEntity>(createRoleDTO);
        await _roleManager.CreateAsync(role);
    }

    public async Task UpdateRoleAsync(string roleId, RoleRequestDTO updateRoleDTO, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(roleId) ??
                   throw new EntityNotFoundException("Role", roleId);
        
        var existingRole = await _roleManager.FindByNameAsync(updateRoleDTO.Name);
        if (existingRole != null && existingRole.Id != role.Id)
        {
            throw new EntityAlreadyExistsException($"Role with name '{updateRoleDTO.Name}' already exists.");
        }

        _mapper.Map(updateRoleDTO, role);
        await _roleManager.UpdateAsync(role);
    }

    public async Task DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(roleId) ??
                   throw new EntityNotFoundException("Role", roleId);

        await _roleManager.DeleteAsync(role);
    }
}