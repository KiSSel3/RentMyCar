using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Infrastructure.Evaluators;
using CarManagementService.Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Repositories.Common;

public class SpecificationQueryRepository<TEntity> : CommandRepository<TEntity>, ISpecificationQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;
    
    public SpecificationQueryRepository(ApplicationDbContext context) : base(context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(_dbSet, specification)
            .Where(e=>!e.IsDeleted)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> FirstOrDefault(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(_dbSet, specification)
            .FirstOrDefaultAsync(e=>!e.IsDeleted, cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(_dbSet, specification)
            .CountAsync(e => !e.IsDeleted, cancellationToken);
    }
}