using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IBrandRepository : IBaseQueryRepository<BrandEntity>
{
    Task<BrandEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}