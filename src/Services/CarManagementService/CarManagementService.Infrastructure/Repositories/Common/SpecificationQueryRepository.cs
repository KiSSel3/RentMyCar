using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Entities;
using CarManagementService.Infrastructure.Infrastructure;
using CarManagementService.Infrastructure.Repositories.Evaluators;
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

    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
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
}