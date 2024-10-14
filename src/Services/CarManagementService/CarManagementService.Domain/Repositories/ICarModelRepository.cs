using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Domain.Repositories;

public interface ICarModelRepository : IBaseQueryRepository<CarModelEntity>
{
    Task<IEnumerable<CarModelEntity>> GetByBrandIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CarModelEntity>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<CarModelEntity> GetByBrandIdAndNameAsync(Guid carBrandId, string name, CancellationToken cancellationToken = default);
}