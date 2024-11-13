using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Publishers.Interfaces;

public interface INotificationPublisher
{
    Task PublishReviewCreatedMessageAsync(ReviewEntity review, RentOfferEntity rentOffer,
        CancellationToken cancellationToken = default);
    
    Task PublishRentOfferCreatedMessageAsync(RentOfferEntity rentOffer, CarEntity carEntity,
        CancellationToken cancellationToken = default);
    
    Task PublishRentOfferDeletedMessageAsync(RentOfferEntity rentOffer, CarEntity carEntity,
        CancellationToken cancellationToken = default);
}