using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByIdSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByIdSpecification(Guid id)
        : base(offer => offer.Id == id)
    {
    }
}