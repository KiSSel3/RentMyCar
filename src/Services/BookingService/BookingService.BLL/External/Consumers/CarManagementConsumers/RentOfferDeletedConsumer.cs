using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.CarManagementService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.External.Consumers.CarManagementConsumers;

public class RentOfferDeletedConsumer : IConsumer<RentOfferDeletedMessage>
{
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger<RentOfferDeletedConsumer> _logger;

    public RentOfferDeletedConsumer(
        INotificationHandler notificationHandler,
        ILogger<RentOfferDeletedConsumer> logger)
    {
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RentOfferDeletedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing RentOfferDeleted message for user ID: {UserId}, car: {CarBrand} {CarModel}", 
            message.UserId,
            message.CarBrand,
            message.CarModel);

        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = $"Your rent offer for {message.CarBrand} {message.CarModel} has been deleted. Contact support if this is an error."
        };

        await _notificationHandler.SendAndPersistAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully processed offer deletion notification for user ID: {UserId}, car: {CarBrand} {CarModel}",
            message.UserId,
            message.CarBrand,
            message.CarModel);
    }
}