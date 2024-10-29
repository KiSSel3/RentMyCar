using MediatR;

namespace CarManagementService.Application.UseCases.CarModel.Commands.CreateCarModel;

public class CreateCarModelCommand : IRequest
{
    public Guid CarBrandId { get; set; }
    public string Name { get; set; }
}