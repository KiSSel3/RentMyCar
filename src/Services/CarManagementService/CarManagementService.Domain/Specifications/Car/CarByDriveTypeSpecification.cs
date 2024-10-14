using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Enums;
using CarManagementService.Domain.Models;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByDriveTypeSpecification : DirectSpecification<CarEntity>
{
    public CarByDriveTypeSpecification(CarDriveType driveType)
        : base(car => car.DriveType == driveType)
    {
    }
}