using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Infrastructure.Infrastructure;
using CarManagementService.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Repositories.Implementations;

public class CarModelRepository : BaseQueryRepository<CarModelEntity>, ICarModelRepository
{
    private readonly ApplicationDbContext _context;
    
    public CarModelRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CarModelEntity> GetByBrandIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CarModels
            .Include(cm => cm.Brand)
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);
    }

    public async Task<CarModelEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.CarModels
            .Include(cm => cm.Brand)
            .FirstOrDefaultAsync(b => b.Name == name && !b.IsDeleted, cancellationToken);
    }
}