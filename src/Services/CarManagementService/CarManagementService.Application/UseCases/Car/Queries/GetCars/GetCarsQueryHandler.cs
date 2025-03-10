using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using CarManagementService.Domain.Specifications.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Car.Queries.GetCars;

public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, PaginatedResult<CarDTO>>
{
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCarsQueryHandler> _logger;

    public GetCarsQueryHandler(
        ICarRepository repository, 
        IMapper mapper,
        ILogger<GetCarsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<CarDTO>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching cars with filters.");
        
        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var spec = CreateSpecification(request);
        
        var totalCount = await _repository.CountAsync(spec, cancellationToken);

        spec = spec.And(new CarPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        
        var cars = await _repository.GetBySpecificationAsync(spec, cancellationToken);

        var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(cars);
        
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize.Value);
        
        var result = new PaginatedResult<CarDTO>
        {
            Collection = carDTOs,
            CurrentPage = request.PageNumber.Value,
            PageSize = request.PageSize.Value,
            TotalPageCount = totalPages,
            TotalItemCount = totalCount
        };

        _logger.LogInformation("Retrieved {CarCount} cars out of {TotalCount} total", cars.Count(), totalCount);
        
        return result;
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