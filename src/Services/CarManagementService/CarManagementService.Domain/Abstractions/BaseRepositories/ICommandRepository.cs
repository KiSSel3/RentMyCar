using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Domain.Abstractions.BaseRepositories;

public interface ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}