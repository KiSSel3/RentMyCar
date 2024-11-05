using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.BLL.External.Publishers.Implementation;
using IdentityService.BLL.External.Publishers.Interfaces;
using IdentityService.BLL.Models.Options;
using IdentityService.BLL.Providers.Implementation;
using IdentityService.BLL.Providers.Interfaces;
using IdentityService.BLL.Services.Implementation;
using IdentityService.BLL.Services.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.BLL;

public static class DependencyInjection
{

    public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.Configure<TokenOptions>(configuration.GetSection(TokenOptions.DefaultSection));

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            
            x.AddScoped<INotificationPublisher, NotificationPublisher>();
            
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]), h =>
                {
                    h.Username(configuration["MessageBroker:Username"]);
                    h.Password(configuration["MessageBroker:Password"]);
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
        
        services.AddScoped<ITokenProvider, TokenProvider>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
            
        services.AddGrpc();
        
        return services;
    }
}