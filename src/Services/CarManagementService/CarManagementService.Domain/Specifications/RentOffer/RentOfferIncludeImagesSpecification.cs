using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferIncludeImagesSpecification : IncludeSpecification<RentOfferEntity>
{
    public RentOfferIncludeImagesSpecification()
        : base(offer => offer.Images)
    {
    }
}