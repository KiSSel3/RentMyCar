using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.CarModel.CreateCarModel;

public class CreateCarModelCommandHandler : IRequestHandler<CreateCarModelCommand>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;

    public CreateCarModelCommandHandler(ICarModelRepository carModelRepository, IMapper mapper)
    {
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateCarModelCommand request, CancellationToken cancellationToken)
    {
        var carModel = await _carModelRepository
            .GetByBrandIdAndNameAsync(request.CarBrandId, request.Name, cancellationToken);
        if (carModel is not null && carModel.CarBrandId != request.CarBrandId)
        {
            throw new EntityNotFoundException($"Car model with Brand ID {request.CarBrandId} and Name {request.Name} was not found.");
        }

        carModel = _mapper.Map<CarModelEntity>(request);
        
        await _carModelRepository.CreateAsync(carModel, cancellationToken);
    }
}