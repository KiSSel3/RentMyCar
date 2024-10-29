using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewByIdSpecification : DirectSpecification<ReviewEntity>
{
    public ReviewByIdSpecification(Guid id)
        : base(review => review.Id == id)
    {
    }
}