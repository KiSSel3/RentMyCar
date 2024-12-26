using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.CarManagementService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.External.Consumers.CarManagementConsumers;

public class RentOfferCreatedConsumer : IConsumer<RentOfferCreatedMessage>
{
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger<RentOfferCreatedConsumer> _logger;

    public RentOfferCreatedConsumer(
        INotificationHandler notificationHandler,
        ILogger<RentOfferCreatedConsumer> logger)
    {
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RentOfferCreatedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing RentOfferCreated message for user ID: {UserId}, car: {CarBrand} {CarModel}", 
            message.UserId,
            message.CarBrand,
            message.CarModel);

        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = $"Your rent offer for {message.CarBrand} {message.CarModel} has been created. Price per day: ${message.PricePerDay}"
        };

        await _notificationHandler.SendAndPersistAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully processed rent offer notification for user ID: {UserId}, car: {CarBrand} {CarModel}",
            message.UserId,
            message.CarBrand,
            message.CarModel);
    }
}