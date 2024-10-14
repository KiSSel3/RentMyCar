using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByYearSpecification : DirectSpecification<CarEntity>
{
    public CarByYearSpecification(DateTime minYear)
        : base(car => car.ModelYear == minYear)
    {
    }
}