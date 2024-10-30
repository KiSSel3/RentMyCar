using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.IdentityService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Consumers.IdentityConsumers;

public class UserDeletedConsumer : IConsumer<UserDeletedMessage>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<UserRegisteredConsumer> _logger;

    public UserDeletedConsumer(
        INotificationRepository notificationRepository,
        ILogger<UserRegisteredConsumer> logger)
    {
        _notificationRepository = notificationRepository;
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
            Message = "Your account has been deleted. We're sorry to see you go. Contact support if this is an error.",
            IsSent = false
        };

        await _notificationRepository.CreateAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully created account deletion notification for user ID: {UserId}",
            message.UserId);
    }
}