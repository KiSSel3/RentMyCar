using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.RentOffer;

public class RentOfferPaginationSpecification : PaginationSpecification<RentOfferEntity>
{
    public RentOfferPaginationSpecification(int pageNumber, int pageSize)
        : base(pageNumber, pageSize)
    {
    }
}