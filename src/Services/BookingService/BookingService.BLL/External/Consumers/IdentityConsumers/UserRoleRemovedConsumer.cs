using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.IdentityService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.External.Consumers.IdentityConsumers;

public class UserRoleRemovedConsumer : IConsumer<UserRoleRemovedMessage>
{
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger<UserRoleRemovedConsumer> _logger;

    public UserRoleRemovedConsumer(
        INotificationHandler notificationHandler,
        ILogger<UserRoleRemovedConsumer> logger)
    {
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserRoleRemovedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing UserRoleRemoved message for user ID: {UserId}, role: {Role}", 
            message.UserId,
            message.Role);

        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = $"The {message.Role} role has been removed from your account. Contact support if this is an error."
        };

        await _notificationHandler.SendAndPersistAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully processed role removal notification for user ID: {UserId}, role: {Role}", 
            message.UserId,
            message.Role);
    }
}