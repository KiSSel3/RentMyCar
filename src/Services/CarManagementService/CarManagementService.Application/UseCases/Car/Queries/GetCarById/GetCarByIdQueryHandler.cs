using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Car.Queries.GetCarById;

public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDTO>
{
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCarByIdQueryHandler> _logger;

    public GetCarByIdQueryHandler(
        ICarRepository repository, 
        IMapper mapper,
        ILogger<GetCarByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CarDTO> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching car with ID: {CarId}", request.CarId);

        var carByIdSpec = new CarByIdSpecification(request.CarId);
        var includeSpec = new CarIncludeAllSpecification();
        
        var combinedSpec = carByIdSpec.And(includeSpec);

        var car = await _repository.FirstOrDefault(combinedSpec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.CarId);
        }

        _logger.LogInformation("Successfully retrieved car with ID: {CarId}", request.CarId);

        return _mapper.Map<CarDTO>(car);
    }
}