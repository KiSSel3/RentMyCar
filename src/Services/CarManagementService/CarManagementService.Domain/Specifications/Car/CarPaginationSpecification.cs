using System.Linq.Expressions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public class CarPaginationSpecification : PaginationSpecification<CarEntity>
{
    public CarPaginationSpecification(int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
    }
}