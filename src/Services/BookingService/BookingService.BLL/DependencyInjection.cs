using System.Reflection;
using BookingService.BLL.Models.Options;
using BookingService.BLL.Providers.Implementations;
using BookingService.BLL.Providers.Interfaces;
using BookingService.BLL.Services.Implementations;
using BookingService.BLL.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.Configure<BookingCacheOptions>(configuration.GetSection(BookingCacheOptions.SectionName));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
            options.InstanceName = configuration.GetSection("Redis")["InstanceName"];
        });
        
        services.AddScoped<IRentOfferProvider, MockRentOfferProvider>();
        services.AddScoped<IUserProvider, MockUserProvider>();
        services.AddScoped<ICacheProvider, CacheProvider>();
        
        services.AddScoped<Services.Implementations.BookingService>();
        services.AddScoped<IBookingService, CachedBookingServiceDecorator>();
            
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
}