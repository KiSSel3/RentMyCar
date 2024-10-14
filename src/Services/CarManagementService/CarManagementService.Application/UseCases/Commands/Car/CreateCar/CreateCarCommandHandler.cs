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
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;

    public CreateCarCommandHandler(ICarRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = _mapper.Map<CarEntity>(request);

        if (request.Image is not null)
        {
            car.Image = await ConvertToByteArrayAsync(request.Image, cancellationToken);
        }
        
        await _repository.CreateAsync(car, cancellationToken);
    }

    private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}