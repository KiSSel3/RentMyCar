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
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCarCommandHandler(ICarRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var spec = new CarByIdSpecification(request.Id);

        var car = await _repository.FirstOrDefault(spec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.Id);
        }

        _mapper.Map(request, car);
        
        if (request.Image is not null)
        {
            car.Image = await ConvertToByteArrayAsync(request.Image, cancellationToken);
        }

        await _repository.UpdateAsync(car, cancellationToken);
    }

    private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}