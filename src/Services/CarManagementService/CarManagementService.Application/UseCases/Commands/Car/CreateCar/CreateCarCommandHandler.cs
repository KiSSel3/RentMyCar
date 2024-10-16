using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.Commands.Car.CreateCar;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;
    
    public CreateCarCommandHandler(
        ICarRepository carRepository,
        ICarModelRepository carModelRepository,
        IMapper mapper)
    {
        _carRepository = carRepository;
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var car = _mapper.Map<CarEntity>(request);

        if (request.Image is not null)
        {
            car.Image = await ConvertToByteArrayAsync(request.Image, cancellationToken);
        }
        
        await _carRepository.CreateAsync(car, cancellationToken);
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