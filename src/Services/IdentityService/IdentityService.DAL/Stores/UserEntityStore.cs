using IdentityService.DAL.Infrastructure;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Stores;

public class UserEntityStore : UserStore<UserEntity, RoleEntity, ApplicationDbContext, Guid>
{
    public UserEntityStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    { }
    
    public override async Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        user.IsDeleted = true;
        
        var result = await UpdateAsync(user, cancellationToken);
        return result;
    }

    public override async Task<UserEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var id = ConvertIdFromString(userId);
        
        var user = await Users.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, cancellationToken);

        return user;
    }

    public override async Task<UserEntity?> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        var user = await Users.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedName && !u.IsDeleted, cancellationToken);

        return user;
    }
}