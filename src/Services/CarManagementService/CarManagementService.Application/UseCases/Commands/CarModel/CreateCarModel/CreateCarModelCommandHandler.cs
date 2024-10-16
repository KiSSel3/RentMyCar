using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.CarModel.CreateCarModel;

public class CreateCarModelCommandHandler : IRequestHandler<CreateCarModelCommand>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public CreateCarModelCommandHandler(
        ICarModelRepository carModelRepository,
        IBrandRepository brandRepository,
        IMapper mapper)
    {
        _carModelRepository = carModelRepository;
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateCarModelCommand request, CancellationToken cancellationToken)
    {
        await EnsureEntityExistsAsync(request, cancellationToken);
        
        var carModel = await _carModelRepository
            .GetByBrandIdAndNameAsync(request.CarBrandId, request.Name, cancellationToken);
        if (carModel is not null)
        {
            throw new EntityAlreadyExistsException($"Car model with Brand ID {request.CarBrandId} and Name {request.Name} already exists.");
        }

        carModel = _mapper.Map<CarModelEntity>(request);
        
        await _carModelRepository.CreateAsync(carModel, cancellationToken);
    }
    
    private async Task EnsureEntityExistsAsync(CreateCarModelCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.CarBrandId);
        }
    }
}