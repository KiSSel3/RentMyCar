using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByUserIdSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByUserIdSpecification(Guid userId)
        : base(offer => offer.UserId == userId)
    {
    }
}