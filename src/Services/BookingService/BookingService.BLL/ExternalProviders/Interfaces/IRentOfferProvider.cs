using BookingService.BLL.Models.Results;

namespace BookingService.BLL.ExternalProviders.Interfaces;

public interface IRentOfferProvider
{
    Task<RentOfferResult> GetRentOfferById(Guid id, CancellationToken cancellationToken = default);
}