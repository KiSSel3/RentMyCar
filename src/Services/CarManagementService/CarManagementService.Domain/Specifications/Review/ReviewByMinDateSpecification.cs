using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewByMinDateSpecification : DirectSpecification<ReviewEntity>
{
    public ReviewByMinDateSpecification(DateTime minDate)
        : base(review => review.CreatedAt >= minDate)
    {
    }
}