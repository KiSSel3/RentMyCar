using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.Car.CreateCar;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCarCommandHandler> _logger;
    
    public CreateCarCommandHandler(
        ICarRepository carRepository,
        ICarModelRepository carModelRepository,
        IMapper mapper,
        ILogger<CreateCarCommandHandler> logger)
    {
        _carRepository = carRepository;
        _carModelRepository = carModelRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create a new car with model ID: {ModelId}", request.ModelId);

        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var car = _mapper.Map<CarEntity>(request);

        if (request.Image is not null)
        {
            _logger.LogInformation("Converting image for car with model ID: {ModelId}", request.ModelId);
            
            car.Image = await ConvertToByteArrayAsync(request.Image, cancellationToken);
        }
        
        await _carRepository.CreateAsync(car, cancellationToken);

        _logger.LogInformation("Successfully created a new car with ID: {CarId}", car.Id);
    }

    private async Task EnsureRelatedEntityExistsAsync(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var carModel = await _carModelRepository.GetByIdAsync(request.ModelId, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException(nameof(CarModelEntity), request.ModelId);
        }
    }
    
    private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}