using CarManagementService.Domain.Repositories;
using CarManagementService.Infrastructure.Infrastructure;
using CarManagementService.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarManagementService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurePostgreSql(services, configuration);

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICarModelRepository, CarModelRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IRentOfferRepository, RentOfferRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        
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