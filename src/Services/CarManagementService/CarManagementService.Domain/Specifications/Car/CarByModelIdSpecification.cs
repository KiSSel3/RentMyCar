using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Models;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public sealed class CarByModelIdSpecification : DirectSpecification<CarEntity>
{
    public CarByModelIdSpecification(Guid modelId)
        : base(car => car.ModelId == modelId)
    {
    }
}