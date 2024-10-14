using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Infrastructure.Infrastructure;
using CarManagementService.Infrastructure.Repositories.Common;

namespace CarManagementService.Infrastructure.Repositories.Implementations;

public class RentOfferRepository : SpecificationQueryRepository<RentOfferEntity>, IRentOfferRepository
{
    public RentOfferRepository(ApplicationDbContext context) : base(context)
    {
    }
}