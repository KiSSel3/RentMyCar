using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewIncludeRentOfferSpecification : IncludeSpecification<ReviewEntity>
{
    public ReviewIncludeRentOfferSpecification()
        : base(review => review.RentOffer)
    {
        AddInclude(review => review.RentOffer.Car);
    }
}