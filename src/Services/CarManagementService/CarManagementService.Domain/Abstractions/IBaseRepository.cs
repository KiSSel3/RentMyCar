using System.Linq.Expressions;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Abstractions;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}