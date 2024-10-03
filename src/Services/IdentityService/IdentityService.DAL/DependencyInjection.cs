using IdentityService.DAL.Infrastructure;
using IdentityService.DAL.Stores;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurePostgreSql(services, configuration);
        ConfigureIdentity(services);

        return services;
    }

    private static void ConfigurePostgreSql(IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        string? dataBaseConnection = configuration.GetConnectionString("PostrgeSql");
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(dataBaseConnection));
    }

    private static void ConfigureIdentity(IServiceCollection services)
    {
        services.AddIdentity<UserEntity, RoleEntity>()
            .AddUserStore<UserEntityStore>()
            .AddRoleStore<RoleEntityStore>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}