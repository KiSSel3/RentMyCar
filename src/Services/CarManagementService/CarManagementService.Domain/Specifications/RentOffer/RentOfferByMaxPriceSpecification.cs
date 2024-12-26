using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByMaxPriceSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByMaxPriceSpecification(decimal maxPrice)
        : base(offer => offer.PricePerDay <= maxPrice)
    {
    }
}