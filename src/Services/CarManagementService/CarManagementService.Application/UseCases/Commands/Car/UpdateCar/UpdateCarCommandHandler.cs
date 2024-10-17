using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.Car.UpdateCar;

public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCarCommandHandler> _logger;

    public UpdateCarCommandHandler(
        ICarRepository carRepository,
        ICarModelRepository carModelRepository,
        IMapper mapper,
        ILogger<UpdateCarCommandHandler> logger)
    {
        _carRepository = carRepository;
        _carModelRepository = carModelRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to update car with ID: {CarId}", request.Id);

        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var spec = new CarByIdSpecification(request.Id);

        var car = await _carRepository.FirstOrDefault(spec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.Id);
        }

        _mapper.Map(request, car);
        
        if (request.Image is not null)
        {
            _logger.LogInformation("Updating image for car with ID: {CarId}", request.Id);
            
            car.Image = await ConvertToByteArrayAsync(request.Image, cancellationToken);
        }

        await _carRepository.UpdateAsync(car, cancellationToken);

        _logger.LogInformation("Successfully updated car with ID: {CarId}", request.Id);
    }

    private async Task EnsureRelatedEntityExistsAsync(UpdateCarCommand request, CancellationToken cancellationToken)
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