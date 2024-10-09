using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface ICarModelRepository : IBaseQueryRepository<CarModelEntity>
{
    Task<BrandEntity> GetByBrandIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BrandEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}