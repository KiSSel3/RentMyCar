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

    public async Task<IEnumerable<CarModelEntity>> GetByBrandIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CarModels
            .Include(cm => cm.Brand)
            .Where(cm => cm.CarBrandId == id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CarModelEntity>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.CarModels
            .Include(cm => cm.Brand)
            .Where(cm => cm.Name == name)
            .ToListAsync(cancellationToken);
    }

    public async Task<CarModelEntity> GetByBrandIdAndNameAsync(Guid carBrandId, string name, CancellationToken cancellationToken = default)
    {
        return await _context.CarModels
            .Include(cm => cm.Brand)
            .FirstOrDefaultAsync(cm => cm.CarBrandId == carBrandId && cm.Name == name,
                cancellationToken);
    }
}