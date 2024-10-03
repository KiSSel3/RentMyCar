using System.Reflection;
using IdentityService.DAL.Infrastructure.Configurations;
using IdentityService.DAL.Infrastructure.Extensions;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.SeedRolesData();
        builder.SeedUsersData();
        builder.SeedUsersRolesData();
    }
}