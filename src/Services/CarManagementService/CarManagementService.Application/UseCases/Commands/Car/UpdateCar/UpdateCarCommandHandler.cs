using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.Commands.Car.UpdateCar;

public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;

    public UpdateCarCommandHandler(
        ICarRepository carRepository,
        ICarModelRepository carModelRepository,
        IMapper mapper)
    {
        _carRepository = carRepository;
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
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
            car.Image = await ConvertToByteArrayAsync(request.Image, cancellationToken);
        }

        await _carRepository.UpdateAsync(car, cancellationToken);
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