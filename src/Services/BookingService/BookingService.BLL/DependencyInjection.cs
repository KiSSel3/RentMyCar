using System.Reflection;
using BookingService.BLL.Consumers.IdentityConsumers;
using BookingService.BLL.External.Implementations;
using BookingService.BLL.External.Interfaces;
using BookingService.BLL.Models.Options;
using BookingService.BLL.Providers.Implementations;
using BookingService.BLL.Providers.Interfaces;
using BookingService.BLL.Services.Implementations;
using BookingService.BLL.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
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
        
        services.AddScoped<IRentOfferService, MockRentOfferService>();
        services.AddScoped<IUserService, MockUserService>();
        services.AddScoped<ICacheProvider, CacheProvider>();
        
        services.AddScoped<Services.Implementations.BookingService>();
        services.AddScoped<IBookingService, CachedBookingServiceDecorator>();
            
        services.AddScoped<INotificationService, NotificationService>();
        
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumers(typeof(UserRegisteredConsumer).Assembly);
    
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]), h =>
                {
                    h.Username(configuration["MessageBroker:Username"]);
                    h.Password(configuration["MessageBroker:Password"]);
                });

                configurator.UseMessageRetry(r =>
                {
                    r.Intervals(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15));
                });

                configurator.UseCircuitBreaker(cb =>
                {
                    cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                    cb.TripThreshold = 15;
                    cb.ActiveThreshold = 10;
                    cb.ResetInterval = TimeSpan.FromMinutes(5);
                });
                
                configurator.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}