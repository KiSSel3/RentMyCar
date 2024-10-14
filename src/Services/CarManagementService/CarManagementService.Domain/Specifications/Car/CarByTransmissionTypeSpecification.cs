using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Enums;
using CarManagementService.Domain.Models;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByTransmissionTypeSpecification : DirectSpecification<CarEntity>
{
    public CarByTransmissionTypeSpecification(CarTransmissionType transmissionType)
        : base(car => car.TransmissionType == transmissionType)
    {
    }
}