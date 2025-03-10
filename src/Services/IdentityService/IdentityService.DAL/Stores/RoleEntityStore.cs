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
        var id = ConvertIdFromString(userId);
        
        var role = await Roles.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, cancellationToken);

        return role;
    }

    public override async Task<RoleEntity?> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        var role = await Roles.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.NormalizedName == normalizedName && !u.IsDeleted, cancellationToken);

        return role;
    }
}