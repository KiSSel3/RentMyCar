using System.Reflection;
using BookingService.BLL.External.Consumers.IdentityConsumers;
using BookingService.BLL.External.Services.Implementations;
using BookingService.BLL.External.Services.Interfaces;
using BookingService.BLL.Features.Booking.Services.Implementations;
using BookingService.BLL.Features.Booking.Services.Interfaces;
using BookingService.BLL.Features.Notifications.BackgroundJobs.Implementations;
using BookingService.BLL.Features.Notifications.BackgroundJobs.Interfaces;
using BookingService.BLL.Features.Notifications.Factories.Implementations;
using BookingService.BLL.Features.Notifications.Factories.Interfaces;
using BookingService.BLL.Features.Notifications.Handlers.Implementations;
using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.BLL.Features.Notifications.Services.Implementations;
using BookingService.BLL.Features.Notifications.Services.Interfaces;
using BookingService.BLL.Infrastructure.Providers.Implementations;
using BookingService.BLL.Infrastructure.Providers.Interfaces;
using BookingService.BLL.Models.Options;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidationServices()
            .AddCacheServices(configuration)
            .AddApplicationServices(configuration)
            .AddMessagingServices(configuration)
            .AddBackgroundJobs(configuration);

        return services;
    }

    private static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }

    private static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BookingCacheOptions>(configuration.GetSection(BookingCacheOptions.SectionName));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
            options.InstanceName = configuration.GetSection("Redis")["InstanceName"];
        });

        services.AddScoped<ICacheProvider, CacheProvider>();

        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRentOfferService, MockRentOfferService>();
        services.AddScoped<IUserService, MockUserService>();
        
        services.AddScoped<Features.Booking.Services.Implementations.BookingService>();
        services.AddScoped<IBookingService, CachedBookingServiceDecorator>();
        
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationHandler, NotificationHandler>();
        
        services.Configure<EmailNotificationOptions>(configuration.GetSection(EmailNotificationOptions.SectionName));
        services.AddScoped<INotificationSender, EmailNotificationSender>();

        services.AddScoped<IBookingNotificationMessageFactory, BookingNotificationMessageFactory>();
        
        return services;
    }

    private static IServiceCollection AddMessagingServices(this IServiceCollection services, IConfiguration configuration)
    {
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
                    r.Intervals(
                        TimeSpan.FromSeconds(5), 
                        TimeSpan.FromSeconds(10), 
                        TimeSpan.FromSeconds(15));
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

    private static IServiceCollection AddBackgroundJobs(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var mongoConfig = configuration.GetSection("MongoDb");
        var connectionString = mongoConfig["ConnectionString"];
        var databaseName = mongoConfig["DatabaseName"];
    
        services.AddHangfire((sp, config) =>
        {
            config.UseMongoStorage(connectionString, databaseName, new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new DropMongoMigrationStrategy(),
                    BackupStrategy = new NoneMongoBackupStrategy()
                },
                Prefix = "hangfire.mongo",
                CheckConnection = true,
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
            });
        });

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = Environment.ProcessorCount * 2;
            options.Queues = new[] { "default" };
            options.ServerTimeout = TimeSpan.FromMinutes(5);
            options.ShutdownTimeout = TimeSpan.FromMinutes(1);
        });

        services.AddScoped<IUnsentNotificationsJob, UnsentNotificationsJob>();
        services.AddScoped<IBookingNotificationScheduler, BookingNotificationScheduler>();

        return services;
    }
}