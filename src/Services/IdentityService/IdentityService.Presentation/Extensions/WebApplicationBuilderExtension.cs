using IdentityService.DAL.Infrastructure;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Presentation.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddIdentity<UserEntity, RoleEntity>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        string? dataBaseConnection = builder.Configuration.GetConnectionString("PostrgeSql");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(dataBaseConnection);
        });
    }
}