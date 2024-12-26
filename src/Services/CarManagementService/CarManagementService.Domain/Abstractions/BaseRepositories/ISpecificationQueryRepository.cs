using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Domain.Abstractions.BaseRepositories;

public interface ISpecificationQueryRepository<TEntity> : ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefault(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}