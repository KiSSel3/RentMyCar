using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.IdentityService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Consumers.IdentityConsumers;

public class UserRegisteredConsumer : IConsumer<UserRegisteredMessage>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<UserRegisteredConsumer> _logger;

    public UserRegisteredConsumer(
        INotificationRepository notificationRepository,
        ILogger<UserRegisteredConsumer> logger)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserRegisteredMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing UserRegistered message for user: {Username} with ID: {UserId}", 
            message.Username, 
            message.UserId);
        
        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = $"Welcome, {message.Username}! Your account has been successfully registered.",
            IsSent = false
        };

        await _notificationRepository.CreateAsync(notification, context.CancellationToken);
        
        _logger.LogInformation("Successfully created welcome notification for user: {Username} with ID: {UserId}", 
            message.Username, 
            message.UserId);
    }
}