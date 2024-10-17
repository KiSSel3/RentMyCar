using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByName;

public class GetCarModelsByNameQueryHandler : IRequestHandler<GetCarModelsByNameQuery, IEnumerable<CarModelDTO>>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCarModelsByNameQueryHandler> _logger;

    public GetCarModelsByNameQueryHandler(
        ICarModelRepository repository, 
        IMapper mapper,
        ILogger<GetCarModelsByNameQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CarModelDTO>> Handle(GetCarModelsByNameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching car models with name: {ModelName}", request.Name);

        var carModels = await _repository.GetByNameAsync(request.Name, cancellationToken);
        
        _logger.LogInformation("Retrieved {CarModelCount} car models with name: {ModelName}", carModels.Count(), request.Name);

        return _mapper.Map<IEnumerable<CarModelDTO>>(carModels);
    }
}