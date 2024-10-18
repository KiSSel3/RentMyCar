using IdentityService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities;

public class RoleEntity : IdentityRole<Guid>, ISoftDelete
{
    public bool IsDeleted { get; set; }
}