using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Repositories.Common;

public class BaseQueryRepository<TEntity> : CommandRepository<TEntity>, IBaseQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;
    
    public BaseQueryRepository(ApplicationDbContext context) : base(context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(e => !e.IsDeleted)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken);
    }
}