using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelsByBrandId;

public class GetCarModelsByBrandIdQueryHandler : IRequestHandler<GetCarModelsByBrandIdQuery, IEnumerable<CarModelDTO>>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCarModelsByBrandIdQueryHandler> _logger;

    public GetCarModelsByBrandIdQueryHandler(
        ICarModelRepository repository, 
        IMapper mapper,
        ILogger<GetCarModelsByBrandIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CarModelDTO>> Handle(GetCarModelsByBrandIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching car models for Brand ID: {BrandId}", request.BrandId);

        var carModels = await _repository.GetByBrandIdAsync(request.BrandId, cancellationToken);
        
        _logger.LogInformation("Retrieved {CarModelCount} car models for Brand ID: {BrandId}", carModels.Count(), request.BrandId);

        return _mapper.Map<IEnumerable<CarModelDTO>>(carModels);
    }
}