using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Enums;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByTransmissionTypeSpecification : DirectSpecification<CarEntity>
{
    public CarByTransmissionTypeSpecification(CarTransmissionType transmissionType)
        : base(car => car.TransmissionType == transmissionType)
    {
    }
}