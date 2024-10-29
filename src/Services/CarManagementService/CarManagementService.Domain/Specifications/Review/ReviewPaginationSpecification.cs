using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Review;

public class ReviewPaginationSpecification : PaginationSpecification<ReviewEntity>
{
    public ReviewPaginationSpecification(int pageNumber, int pageSize)
        : base(pageNumber, pageSize)
    {
    }
}