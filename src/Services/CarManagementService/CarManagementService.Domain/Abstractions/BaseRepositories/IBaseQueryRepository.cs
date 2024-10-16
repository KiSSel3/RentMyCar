using System.Linq.Expressions;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Domain.Abstractions.BaseRepositories;

public interface IBaseQueryRepository<TEntity> : ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
}