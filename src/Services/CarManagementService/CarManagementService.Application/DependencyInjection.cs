using System.Reflection;
using CarManagementService.Application.Consumers;
using CarManagementService.Application.Publishers.Implementations;
using CarManagementService.Application.Publishers.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarManagementService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            
            x.AddConsumers(typeof(UserDeletedConsumer).Assembly);
            
            x.AddScoped<INotificationPublisher, NotificationPublisher>();
            
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]), h =>
                {
                    h.Username(configuration["MessageBroker:Username"]);
                    h.Password(configuration["MessageBroker:Password"]);
                });

                configurator.ReceiveEndpoint("user-deleted-rent-offers-queue", e =>
                {
                    e.ConfigureConsumer<UserDeletedConsumer>(context);
                });
                
                configurator.ConfigurePublish(p =>
                {
                    p.UseRetry(r =>
                    {
                        r.Interval(3, TimeSpan.FromMinutes(1));
                    });
                    
                    p.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });
                });
                
                configurator.ConfigureEndpoints(context);
            });
        });
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        return services;
    }
}