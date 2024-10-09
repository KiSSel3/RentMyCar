using CarManagementService.Domain.Abstractions;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IRentOfferRepository : IBaseRepository<RentOfferEntity>
{
    Task<IEnumerable<RentOfferEntity>> GetAllAsync(ISpecification<RentOfferEntity> specification, CancellationToken cancellationToken = default);
    Task<RentOfferEntity> FirstOrDefault(ISpecification<RentOfferEntity> specification, CancellationToken cancellationToken = default);

    Task AddImageAsync(Guid rentOfferId, byte[] image, CancellationToken cancellationToken = default);
    Task RemoveImageAsync(Guid rentOfferId, Guid imageId, CancellationToken cancellationToken = default);
}