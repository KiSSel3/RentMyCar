using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByStreetSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByStreetSpecification(string street)
        : base(offer => offer.LocationModel.Street.ToLower().Contains(street.ToLower()))
    {
    }
}