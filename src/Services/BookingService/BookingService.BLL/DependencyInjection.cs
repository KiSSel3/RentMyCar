using System.Reflection;
using BookingService.BLL.ExternalProviders.Implementations;
using BookingService.BLL.ExternalProviders.Interfaces;
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

        services.AddScoped<IRentOfferProvider, MockRentOfferProvider>();
        services.AddScoped<IUserProvider, MockUserProvider>();
        
        services.AddScoped<IBookingService, Services.Implementations.BookingService>();
        services.AddScoped<INotificationService, NotificationService>();
            
        return services;
    }
}