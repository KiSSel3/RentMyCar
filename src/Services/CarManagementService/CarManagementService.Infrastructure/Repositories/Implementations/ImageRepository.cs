using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Repositories.Implementations;

public class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _context;

    public ImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddImageAsync(ImageEntity image, CancellationToken cancellationToken = default)
    {
        await _context.Images.AddAsync(image, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddImagesAsync(IEnumerable<ImageEntity> images, CancellationToken cancellationToken = default)
    {
        await _context.Images.AddRangeAsync(images, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveImageAsync(ImageEntity image, CancellationToken cancellationToken = default)
    {
        _context.Images.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveImagesAsync(IEnumerable<ImageEntity> images, CancellationToken cancellationToken = default)
    {
        _context.Images.RemoveRange(images);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ImageEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Images
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}