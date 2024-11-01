using BookingService.BLL.Models.Results;

namespace BookingService.BLL.External.Services.Interfaces;

public interface IRentOfferService
{
    Task<RentOfferResult> GetRentOfferById(Guid id, CancellationToken cancellationToken = default);
}