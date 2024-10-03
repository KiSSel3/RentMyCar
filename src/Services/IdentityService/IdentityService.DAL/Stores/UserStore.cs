using IdentityService.DAL.Infrastructure;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Stores;

public class UserStore : UserStore<UserEntity, RoleEntity, ApplicationDbContext, Guid>
{
    public UserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    { }
    
    public override async Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        user.IsDeleted = true;
        
        var result = await UpdateAsync(user, cancellationToken);
        return result;
    }

    public override async Task<UserEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        if (Guid.TryParse(userId, out var id))
        {
            return await Users.IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, cancellationToken);
        }

        return null;
    }

    public override async Task<UserEntity?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var users = await Users.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.UserName == name && !u.IsDeleted, cancellationToken);

        return users;
    }
}