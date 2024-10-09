using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IRentOfferRepository : ISpecificationQueryRepository<RentOfferEntity>
{
    Task AddImageAsync(Guid rentOfferId, byte[] image, CancellationToken cancellationToken = default);
    Task RemoveImageAsync(Guid rentOfferId, Guid imageId, CancellationToken cancellationToken = default);
}