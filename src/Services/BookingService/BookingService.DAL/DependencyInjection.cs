using BookingService.DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using BookingService.DAL.Infrastructure.Options;
using BookingService.DAL.Repositories.Implementations;
using BookingService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace BookingService.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        
        services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.DefaultSection));
        
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        
        return services;
    }
}