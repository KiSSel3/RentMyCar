using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.CarModel.CreateCarModel;

public class CreateCarModelCommandHandler : IRequestHandler<CreateCarModelCommand>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCarModelCommandHandler> _logger;

    public CreateCarModelCommandHandler(
        ICarModelRepository carModelRepository,
        IBrandRepository brandRepository,
        IMapper mapper,
        ILogger<CreateCarModelCommandHandler> logger)
    {
        _carModelRepository = carModelRepository;
        _brandRepository = brandRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateCarModelCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create a new car model for Brand ID: {BrandId}, Name: {ModelName}", request.CarBrandId, request.Name);

        await EnsureEntityExistsAsync(request, cancellationToken);
        
        var carModel = await _carModelRepository
            .GetByBrandIdAndNameAsync(request.CarBrandId, request.Name, cancellationToken);
        if (carModel is not null)
        {
            throw new EntityAlreadyExistsException($"Car model with Brand ID {request.CarBrandId} and Name {request.Name} already exists.");
        }
        
        carModel = _mapper.Map<CarModelEntity>(request);
        
        await _carModelRepository.CreateAsync(carModel, cancellationToken);

        _logger.LogInformation("Successfully created new car model with ID: {ModelId} for Brand ID: {BrandId}, Name: {ModelName}", carModel.Id, request.CarBrandId, request.Name);
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