using IdentityService.DAL.Infrastructure;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Stores;

public class RoleEntityStore : RoleStore<RoleEntity, ApplicationDbContext, Guid>
{
    public RoleEntityStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    { }

    public override async Task<IdentityResult> DeleteAsync(RoleEntity role, CancellationToken cancellationToken = default)
    {
        role.IsDeleted = true;
        
        var result = await UpdateAsync(role, cancellationToken);
        return result;
    }
    
    public override async Task<RoleEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        if (Guid.TryParse(userId, out var id))
        {
            return await Roles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, cancellationToken);
        }

        return null;
    }

    public override async Task<RoleEntity?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var users = await Roles.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Name == name && !u.IsDeleted, cancellationToken);

        return users;
    }
}