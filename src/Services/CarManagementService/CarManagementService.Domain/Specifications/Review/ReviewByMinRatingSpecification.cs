using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewByMinRatingSpecification : DirectSpecification<ReviewEntity>
{
    public ReviewByMinRatingSpecification(int minRating)
        : base(review => review.Rating >= minRating)
    {
    }
}