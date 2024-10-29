using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewByMaxDateSpecification : DirectSpecification<ReviewEntity>
{
    public ReviewByMaxDateSpecification(DateTime maxDate)
        : base(review => review.CreatedAt <= maxDate)
    {
    }
}