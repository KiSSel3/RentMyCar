using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.CarManagementService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Consumers.CarManagementConsumers;

public class RentOfferDeletedConsumer : IConsumer<RentOfferDeletedMessage>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<RentOfferDeletedConsumer> _logger;

    public RentOfferDeletedConsumer(
        INotificationRepository notificationRepository,
        ILogger<RentOfferDeletedConsumer> logger)
    {
        _notificationRepository = notificationRepository;
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
            Message = $"Your rent offer for {message.CarBrand} {message.CarModel} has been deleted. Contact support if this is an error.",
            IsSent = false
        };

        await _notificationRepository.CreateAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully created offer deletion notification for user ID: {UserId}, car: {CarBrand} {CarModel}",
            message.UserId,
            message.CarBrand,
            message.CarModel);
    }
}