using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using CarManagementService.Domain.Specifications.Common;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Car.GetCars;

public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, PagedList<CarDTO>>
{
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;

    public GetCarsQueryHandler(ICarRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedList<CarDTO>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        var specification = CreateSpecification(request);
        
        var cars = await _repository.GetAllAsync(specification, cancellationToken);
        
        var pagedList = new PagedList<CarEntity>(cars, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);

        return _mapper.Map<PagedList<CarDTO>>(pagedList);
    }

    private ISpecification<CarEntity> CreateSpecification(GetCarsQuery request)
    {
        BaseSpecification<CarEntity> spec = new CarIncludeAllSpecification();

        if (request.ModelId.HasValue)
        {
            spec = spec.And(new CarByModelIdSpecification(request.ModelId.Value));
        }

        if (request.BodyType.HasValue)
        {
            spec = spec.And(new CarByBodyTypeSpecification(request.BodyType.Value));
        }

        if (request.DriveType.HasValue)
        {
            spec = spec.And(new CarByDriveTypeSpecification(request.DriveType.Value));
        }

        if (request.TransmissionType.HasValue)
        {
            spec = spec.And(new CarByTransmissionTypeSpecification(request.TransmissionType.Value));
        }

        if (request.Year.HasValue)
        {
            spec = spec.And(new CarByYearSpecification(request.Year.Value));
        }
        else
        {
            if (request.MinYear.HasValue)
            {
                spec = spec.And(new CarByMinYearSpecification(request.MinYear.Value));
            }

            if (request.MaxYear.HasValue)
            {
                spec = spec.And(new CarByMaxYearSpecification(request.MaxYear.Value));
            }
        }

        if (request.PageNumber.HasValue && request.PageSize.HasValue)
        {
            spec = spec.And(new CarPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        }

        return spec;
    }
}