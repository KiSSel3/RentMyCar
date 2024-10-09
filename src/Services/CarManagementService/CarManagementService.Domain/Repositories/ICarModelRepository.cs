using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface ICarModelRepository : IBaseQueryRepository<CarModelEntity>
{
    Task<CarModelEntity> GetByBrandIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CarModelEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}