using MediatR;

namespace CarManagementService.Application.UseCases.CarModel.Commands.UpdateCarModel;

public class UpdateCarModelCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CarBrandId { get; set; }
    public string Name { get; set; }
}