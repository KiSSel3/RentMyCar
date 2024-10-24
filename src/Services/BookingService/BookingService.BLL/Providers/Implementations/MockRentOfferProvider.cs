using BookingService.BLL.Exceptions;
using BookingService.BLL.Models.Results;
using BookingService.BLL.Providers.Interfaces;

namespace BookingService.BLL.Providers.Implementations;

public class MockRentOfferProvider : IRentOfferProvider
{
    private readonly IEnumerable<RentOfferResult> _rentOfferResults;

    public MockRentOfferProvider()
    {
        _rentOfferResults = new List<RentOfferResult>()
        {
            new RentOfferResult()
            {
                Id = new Guid("64c78fb5-7421-4e3c-905d-0146758ca4fe"),
                UserId = new Guid("9AB61044-85F6-4C5E-A93A-D860EFAE0CCE"),
                AvailableFrom = new DateTime(2024, 10, 15),
                AvailableTo = new DateTime(2024, 11, 15),
                PricePerDay = 200.00m,
                IsAvailable = true
            }
        };
    }

    public Task<RentOfferResult> GetRentOfferById(Guid id, CancellationToken cancellationToken = default)
    {
        var rentOffer = _rentOfferResults.FirstOrDefault(ror => ror.Id == id);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException("RentOffer", id);
        }
        
        return Task.FromResult(rentOffer);
    }
}