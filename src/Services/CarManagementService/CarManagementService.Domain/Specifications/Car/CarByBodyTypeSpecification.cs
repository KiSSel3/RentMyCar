using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Enums;
using CarManagementService.Domain.Models;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByBodyTypeSpecification : DirectSpecification<CarEntity>
{
    public CarByBodyTypeSpecification(CarBodyType bodyType)
        : base(car => car.BodyType == bodyType)
    {
    }
}