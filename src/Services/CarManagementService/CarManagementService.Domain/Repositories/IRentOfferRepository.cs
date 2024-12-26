using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Domain.Repositories;

public interface IRentOfferRepository : ISpecificationQueryRepository<RentOfferEntity>
{
    
}