using MediatR;

namespace CarManagementService.Application.UseCases.Commands.CarModel.DeleteCarModel;

public class DeleteCarModelCommand : IRequest
{
    public Guid Id { get; set; }
}