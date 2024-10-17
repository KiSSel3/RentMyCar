using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
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
        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var spec = CreateSpecification(request);
        
        var totalCount = await _repository.CountAsync(spec, cancellationToken);

        spec = spec.And(new CarPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        
        var cars = await _repository.GetBySpecificationAsync(spec, cancellationToken);
        
        var pagedList = new PagedList<CarEntity>(cars, totalCount, request.PageNumber.Value, request.PageSize.Value);

        return _mapper.Map<PagedList<CarDTO>>(pagedList);
    }

    private BaseSpecification<CarEntity> CreateSpecification(GetCarsQuery request)
    {
        var spec = new CarIncludeAllSpecification() as BaseSpecification<CarEntity>;

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

        return spec;
    }
}