using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByAvailabilitySpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByAvailabilitySpecification(bool isAvailable)
        : base(offer => offer.IsAvailable == isAvailable)
    {
    }
}