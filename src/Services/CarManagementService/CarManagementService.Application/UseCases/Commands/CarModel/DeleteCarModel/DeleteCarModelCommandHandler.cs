using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.CarModel.DeleteCarModel;

public class DeleteCarModelCommandHandler : IRequestHandler<DeleteCarModelCommand>
{
    private readonly ICarModelRepository _carModelRepository;

    public DeleteCarModelCommandHandler(ICarModelRepository carModelRepository)
    {
        _carModelRepository = carModelRepository;
    }

    public async Task Handle(DeleteCarModelCommand request, CancellationToken cancellationToken)
    {
        var carModel = await _carModelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException(nameof(CarModelEntity), request.Id);
        }

        await _carModelRepository.DeleteAsync(carModel, cancellationToken);
    }
}