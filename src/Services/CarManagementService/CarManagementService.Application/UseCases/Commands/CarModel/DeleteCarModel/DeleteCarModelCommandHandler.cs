using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.CarModel.DeleteCarModel;

public class DeleteCarModelCommandHandler : IRequestHandler<DeleteCarModelCommand>
{
    private readonly ICarModelRepository _repository;

    public DeleteCarModelCommandHandler(ICarModelRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCarModelCommand request, CancellationToken cancellationToken)
    {
        var carModel = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException(nameof(CarModelEntity), request.Id);
        }

        await _repository.DeleteAsync(carModel, cancellationToken);
    }
}