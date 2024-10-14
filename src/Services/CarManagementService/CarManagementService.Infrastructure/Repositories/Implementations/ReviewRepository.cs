using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Infrastructure.Infrastructure;
using CarManagementService.Infrastructure.Repositories.Common;

namespace CarManagementService.Infrastructure.Repositories.Implementations;

public class ReviewRepository : SpecificationQueryRepository<ReviewEntity>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
    }
}