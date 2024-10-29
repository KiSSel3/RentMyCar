using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByMinYearSpecification : DirectSpecification<CarEntity>
{
    public CarByMinYearSpecification(DateTime minYear)
        : base(car => car.ModelYear >= minYear)
    {
    }
}