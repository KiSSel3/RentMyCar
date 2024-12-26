using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Publishers.Interfaces;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using CarManagementService.Domain.Specifications.RentOffer;
using Contracts.Messages.CarManagementService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.Publishers.Implementations;

public class NotificationPublisher : INotificationPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<NotificationPublisher> _logger;

    public NotificationPublisher(IPublishEndpoint publishEndpoint, ILogger<NotificationPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishReviewCreatedMessageAsync(ReviewEntity review, RentOfferEntity rentOffer,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing ReviewCreatedMessage for review {ReviewId}", review.Id);
        
        var message = new ReviewCreatedMessage()
        {
            UserId = rentOffer.UserId,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = DateTime.Now
        };

        await _publishEndpoint.Publish(message, cancellationToken);
        
        _logger.LogInformation("Successfully published ReviewCreatedMessage for review {ReviewId}", review.Id);
    }

    public async Task PublishRentOfferCreatedMessageAsync(RentOfferEntity rentOffer, CarEntity car,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing RentOfferCreatedMessage for rent offer {RentOfferId}", rentOffer.Id);
        
        var message = new RentOfferCreatedMessage()
        {
            UserId = rentOffer.UserId,
            CarBrand = car.CarModel.Brand.Name,
            CarModel = car.CarModel.Name,
            PricePerDay = rentOffer.PricePerDay,
            CreatedAt = DateTime.Now
        };

        await _publishEndpoint.Publish(message, cancellationToken);
        
        _logger.LogInformation("Successfully published RentOfferCreatedMessage for rent offer {RentOfferId}", rentOffer.Id);
    }

    public async Task PublishRentOfferDeletedMessageAsync(RentOfferEntity rentOffer, CarEntity car,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing RentOfferDeletedMessage for rent offer {RentOfferId}", rentOffer.Id);
        
        var message = new RentOfferDeletedMessage()
        {
            UserId = rentOffer.UserId,
            CarBrand = car.CarModel.Brand.Name,
            CarModel = car.CarModel.Name,
            CreatedAt = DateTime.Now
        };

        await _publishEndpoint.Publish(message, cancellationToken);
        
        _logger.LogInformation("Successfully published RentOfferDeletedMessage for rent offer {RentOfferId}", rentOffer.Id);
    }
}