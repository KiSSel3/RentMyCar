using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using Contracts.Messages.CarManagementService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Consumers.CarManagementConsumers;

public class RentOfferCreatedConsumer : IConsumer<RentOfferCreatedMessage>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<RentOfferCreatedConsumer> _logger;

    public RentOfferCreatedConsumer(
        INotificationRepository notificationRepository,
        ILogger<RentOfferCreatedConsumer> logger)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RentOfferCreatedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Processing RentOfferCreated message for user ID: {UserId}, car: {CarBrand} {CarModel}", 
            message.UserId,
            message.CarBrand,
            message.CarModel);

        var notification = new NotificationEntity
        {
            UserId = message.UserId,
            CreatedAt = message.CreatedAt,
            Message = $"Your rent offer for {message.CarBrand} {message.CarModel} has been created. Price per day: ${message.PricePerDay}",
            IsSent = false
        };

        await _notificationRepository.CreateAsync(notification, context.CancellationToken);

        _logger.LogInformation("Successfully created rent offer notification for user ID: {UserId}, car: {CarBrand} {CarModel}",
            message.UserId,
            message.CarBrand,
            message.CarModel);
    }
}