using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByMinPriceSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByMinPriceSpecification(decimal minPrice)
        : base(offer => offer.PricePerDay >= minPrice)
    {
    }
}