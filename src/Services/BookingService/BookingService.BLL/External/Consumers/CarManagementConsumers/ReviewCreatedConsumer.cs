using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.CarManagementService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.External.Consumers.CarManagementConsumers;

public class ReviewCreatedConsumer : IConsumer<ReviewCreatedMessage>
{
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger<ReviewCreatedConsumer> _logger;

    public ReviewCreatedConsumer(
        INotificationHandler notificationHandler,
        ILogger<ReviewCreatedConsumer> logger)
    {
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReviewCreatedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing ReviewCreated message for user ID: {UserId}, rating: {Rating}", 
            message.UserId,
            message.Rating);

        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = $"A new review has been posted with rating {message.Rating}/5. Comment: {message.Comment}"
        };

        await _notificationHandler.SendAndPersistAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully processed review notification for user ID: {UserId}, rating: {Rating}",
            message.UserId,
            message.Rating);
    }
}