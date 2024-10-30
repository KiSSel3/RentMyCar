using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.IdentityService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Consumers.IdentityConsumers;

public class UserRoleRemovedConsumer : IConsumer<UserRoleRemovedMessage>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<UserRoleRemovedConsumer> _logger;

    public UserRoleRemovedConsumer(
        INotificationRepository notificationRepository,
        ILogger<UserRoleRemovedConsumer> logger)
    {
        _notificationRepository = notificationRepository;
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
            Message = $"The {message.Role} role has been removed from your account. Contact support if this is an error.",
            IsSent = false
        };

        await _notificationRepository.CreateAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully created role removal notification for user ID: {UserId}, role: {Role}", 
            message.UserId,
            message.Role);
    }
}