using CarManagementService.Domain.Abstractions.Services;
using CarManagementService.Domain.Repositories;
using CarManagementService.Infrastructure.Infrastructure;
using CarManagementService.Infrastructure.Options;
using CarManagementService.Infrastructure.Repositories.Implementations;
using CarManagementService.Infrastructure.Services;
using CarManagementService.Infrastructure.Services.Client;
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

        services.AddScoped<IUserService, GRPCUserService>();
        
        services.Configure<GRPCOptions>(configuration.GetSection(GRPCOptions.SectionName));

        services.AddGrpc();
        
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