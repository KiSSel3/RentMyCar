using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByCitySpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByCitySpecification(string city)
        : base(offer => offer.LocationModel.City.ToLower().Contains(city.ToLower()))
    {
    }
}