using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Enums;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByBodyTypeSpecification : DirectSpecification<CarEntity>
{
    public CarByBodyTypeSpecification(CarBodyType bodyType)
        : base(car => car.BodyType == bodyType)
    {
    }
}