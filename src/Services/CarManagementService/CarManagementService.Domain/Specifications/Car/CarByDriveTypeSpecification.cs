using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Enums;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByDriveTypeSpecification : DirectSpecification<CarEntity>
{
    public CarByDriveTypeSpecification(CarDriveType driveType)
        : base(car => car.DriveType == driveType)
    {
    }
}