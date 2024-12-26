using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByCarIdSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByCarIdSpecification(Guid carId)
        : base(offer => offer.CarId == carId)
    {
    }
}