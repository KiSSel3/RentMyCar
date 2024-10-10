using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.CarModel.UpdateCarModel;

public class UpdateCarModelCommandHandler : IRequestHandler<UpdateCarModelCommand>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;

    public UpdateCarModelCommandHandler(ICarModelRepository carModelRepository, IMapper mapper)
    {
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCarModelCommand request, CancellationToken cancellationToken)
    {
        var carModel = await _carModelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException(nameof(CarModelEntity), request.Id);
        }

        var existingModel = await _carModelRepository
            .GetByBrandIdAndNameAsync(request.CarBrandId, request.Name, cancellationToken);
        if (existingModel is not null && existingModel.Id != request.Id)
        {
            throw new EntityAlreadyExistsException($"Car model with Brand ID {request.CarBrandId} and Name {request.Name} already exists.");
        }

        _mapper.Map(request, carModel);

        await _carModelRepository.UpdateAsync(carModel, cancellationToken);
    }
}