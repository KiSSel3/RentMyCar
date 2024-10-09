using CarManagementService.Domain.Abstractions;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IBrandRepository : IBaseRepository<BrandEntity>
{
    Task<BrandEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BrandEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<BrandEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}