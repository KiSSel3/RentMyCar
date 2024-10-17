using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetAllCarModels;

public class GetAllCarModelsQueryHandler : IRequestHandler<GetAllCarModelsQuery, IEnumerable<CarModelDTO>>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCarModelsQueryHandler> _logger;

    public GetAllCarModelsQueryHandler(
        ICarModelRepository repository, 
        IMapper mapper,
        ILogger<GetAllCarModelsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CarModelDTO>> Handle(GetAllCarModelsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all car models.");

        var carModels = await _repository.GetAllAsync(cancellationToken, entity => entity.Brand);
        
        var carModelDTOs = _mapper.Map<IEnumerable<CarModelDTO>>(carModels);

        _logger.LogInformation("Retrieved {CarModelCount} car models.", carModelDTOs.Count());

        return carModelDTOs;
    }
}