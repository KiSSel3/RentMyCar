using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewByReviewerIdSpecification : DirectSpecification<ReviewEntity>
{
    public ReviewByReviewerIdSpecification(Guid reviewerId)
        : base(review => review.ReviewerId == reviewerId)
    {
    }
}