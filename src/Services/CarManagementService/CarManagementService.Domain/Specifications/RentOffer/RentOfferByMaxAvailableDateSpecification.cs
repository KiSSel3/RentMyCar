using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByMaxAvailableDateSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByMaxAvailableDateSpecification(DateTime maxDate)
        : base(offer => offer.AvailableFrom <= maxDate)
    {
    }
}