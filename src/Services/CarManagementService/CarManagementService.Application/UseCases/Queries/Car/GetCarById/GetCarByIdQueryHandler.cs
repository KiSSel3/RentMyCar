using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Car.GetCarById;

public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDTO>
{
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;

    public GetCarByIdQueryHandler(ICarRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CarDTO> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var carByIdSpec = new CarByIdSpecification(request.CarId);
        var includeSpec = new CarIncludeAllSpecification();
        var combinedSpec = carByIdSpec.And(includeSpec);

        var car = await _repository.FirstOrDefault(combinedSpec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.CarId);
        }

        return _mapper.Map<CarDTO>(car);
    }
}