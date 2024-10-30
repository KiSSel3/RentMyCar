using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.CarManagementService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Consumers.CarManagementConsumers;

public class ReviewCreatedConsumer : IConsumer<ReviewCreatedMessage>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<ReviewCreatedConsumer> _logger;

    public ReviewCreatedConsumer(
        INotificationRepository notificationRepository,
        ILogger<ReviewCreatedConsumer> logger)
    {
        _notificationRepository = notificationRepository;
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
            Message = $"A new review has been posted with rating {message.Rating}/5. Comment: {message.Comment}",
            IsSent = false
        };

        await _notificationRepository.CreateAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully created review notification for user ID: {UserId}, rating: {Rating}",
            message.UserId,
            message.Rating);
    }
}