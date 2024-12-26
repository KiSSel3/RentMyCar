using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.IdentityService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.External.Consumers.IdentityConsumers;

public class UserDeletedConsumer : IConsumer<UserDeletedMessage>
{
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger<UserDeletedConsumer> _logger;

    public UserDeletedConsumer(
        INotificationHandler notificationHandler,
        ILogger<UserDeletedConsumer> logger)
    {
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing UserDeleted message for user ID: {UserId}", message.UserId);

        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = "Your account has been deleted. We're sorry to see you go. Contact support if this is an error."
        };

        await _notificationHandler.SendAndPersistAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully processed account deletion notification for user ID: {UserId}",
            message.UserId);
    }
}