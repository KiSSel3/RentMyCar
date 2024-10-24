using BookingService.BLL.Models.Results;

namespace BookingService.BLL.Providers.Interfaces;

public interface IRentOfferProvider
{
    Task<RentOfferResult> GetRentOfferById(Guid id, CancellationToken cancellationToken = default);
}