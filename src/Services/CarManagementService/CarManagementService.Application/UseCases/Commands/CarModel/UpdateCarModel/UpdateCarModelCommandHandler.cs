using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.CarModel.UpdateCarModel;

public class UpdateCarModelCommandHandler : IRequestHandler<UpdateCarModelCommand>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCarModelCommandHandler> _logger;

    public UpdateCarModelCommandHandler(
        ICarModelRepository carModelRepository,
        IBrandRepository brandRepository,
        IMapper mapper,
        ILogger<UpdateCarModelCommandHandler> logger)
    {
        _carModelRepository = carModelRepository;
        _brandRepository = brandRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateCarModelCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to update car model with ID: {CarModelId}", request.Id);

        await EnsureEntityExistsAsync(request, cancellationToken);
        
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

        _logger.LogInformation("Successfully updated car model with ID: {CarModelId}", request.Id);
    }
    
    private async Task EnsureEntityExistsAsync(UpdateCarModelCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.CarBrandId);
        }
    }
}