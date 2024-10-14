using System.Linq.Expressions;
using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Models;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarIncludeAllSpecification : IncludeSpecification<CarEntity>
{
    public CarIncludeAllSpecification() : base(c => c.CarModel)
    {
        AddInclude(c => c.CarModel.Brand);
    }
}