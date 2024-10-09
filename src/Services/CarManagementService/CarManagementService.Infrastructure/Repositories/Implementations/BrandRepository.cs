using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Infrastructure.Infrastructure;
using CarManagementService.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Repositories.Implementations;

public class BrandRepository : BaseQueryRepository<BrandEntity>, IBrandRepository
{
    private readonly ApplicationDbContext _context;
    
    public BrandRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BrandEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Brands
            .FirstOrDefaultAsync(b => b.Name == name && !b.IsDeleted, cancellationToken);
    }
}