using BookingService.BLL.Handlers.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.IdentityService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Consumers.IdentityConsumers;

public class UserRoleAssignedConsumer : IConsumer<UserRoleAssignedMessage>
{
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger<UserRoleAssignedConsumer> _logger;

    public UserRoleAssignedConsumer(
        INotificationHandler notificationHandler,
        ILogger<UserRoleAssignedConsumer> logger)
    {
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserRoleAssignedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing UserRoleAssigned message for user ID: {UserId}, role: {Role}", 
            message.UserId,
            message.Role);

        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = $"You have been assigned the {message.Role} role."
        };

        await _notificationHandler.SendAndPersistAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully processed role assignment notification for user ID: {UserId}, role: {Role}", 
            message.UserId,
            message.Role);
    }
}