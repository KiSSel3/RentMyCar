using CarManagementService.Domain.Abstractions;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IReviewRepository : IBaseRepository<ReviewEntity>
{
    Task<IEnumerable<CarEntity>> GetAllAsync(ISpecification<CarEntity> specification, CancellationToken cancellationToken = default);
    Task<CarEntity> FirstOrDefault(ISpecification<CarEntity> specification, CancellationToken cancellationToken = default); 
}