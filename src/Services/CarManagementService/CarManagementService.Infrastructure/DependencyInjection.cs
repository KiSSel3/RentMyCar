using CarManagementService.Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarManagementService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurePostgreSql(services, configuration);

        return services;
    }

    private static void ConfigurePostgreSql(IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        string? dataBaseConnection = configuration.GetConnectionString("PostrgeSql");
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(dataBaseConnection));
    }
}