using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Specifications.Common;

namespace CarManagementService.Domain.Specifications.Car;

public class CarByIdSpecification  : DirectSpecification<CarEntity>
{
    public CarByIdSpecification(Guid id)
        : base(car => car.Id == id)
    {
    }
}