using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IImageRepository
{
    Task AddImageAsync(ImageEntity image, CancellationToken cancellationToken = default);
    Task AddImagesAsync(IEnumerable<ImageEntity> images, CancellationToken cancellationToken = default);
    Task RemoveImageAsync(ImageEntity image, CancellationToken cancellationToken = default);
    Task RemoveImagesAsync(IEnumerable<ImageEntity> images, CancellationToken cancellationToken = default);
    Task<ImageEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}