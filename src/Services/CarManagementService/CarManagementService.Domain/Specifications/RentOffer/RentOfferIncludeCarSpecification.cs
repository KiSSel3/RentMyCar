using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferIncludeCarSpecification : IncludeSpecification<RentOfferEntity>
{
    public RentOfferIncludeCarSpecification()
        : base(offer => offer.Car)
    {
        AddInclude(offer => offer.Car.CarModel);
        AddInclude(offer => offer.Car.CarModel.Brand);
    }
}