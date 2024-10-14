using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferByMinAvailableDateSpecification : DirectSpecification<RentOfferEntity>
{
    public RentOfferByMinAvailableDateSpecification(DateTime minDate)
        : base(offer => offer.AvailableFrom <= minDate && offer.AvailableTo >= minDate)
    {
    }
}