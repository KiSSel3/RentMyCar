using MediatR;

namespace CarManagementService.Application.UseCases.CarModel.Commands.DeleteCarModel;

public class DeleteCarModelCommand : IRequest
{
    public Guid Id { get; set; }
}