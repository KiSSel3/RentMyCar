using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByMaxYearSpecification : DirectSpecification<CarEntity>
{
    public CarByMaxYearSpecification(DateTime maxYear)
        : base(car => car.ModelYear <= maxYear)
    {
    }
}