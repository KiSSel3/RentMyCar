using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Abstractions.BaseRepositories;

public interface IBaseQueryRepository<TEntity> : ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}