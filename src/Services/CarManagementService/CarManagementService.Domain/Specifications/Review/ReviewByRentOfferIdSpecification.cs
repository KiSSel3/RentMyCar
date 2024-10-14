using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewByRentOfferIdSpecification : DirectSpecification<ReviewEntity>
{
    public ReviewByRentOfferIdSpecification(Guid rentOfferId)
        : base(review => review.RentOfferId == rentOfferId)
    {
    }
}