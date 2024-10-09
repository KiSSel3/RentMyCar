using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IReviewRepository : ISpecificationQueryRepository<ReviewEntity>
{
    
}