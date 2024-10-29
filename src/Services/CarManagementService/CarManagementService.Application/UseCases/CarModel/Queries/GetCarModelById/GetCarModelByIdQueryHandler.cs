using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelById;

public class GetCarModelByIdQueryHandler : IRequestHandler<GetCarModelByIdQuery, CarModelDTO>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCarModelByIdQueryHandler> _logger;

    public GetCarModelByIdQueryHandler(
        ICarModelRepository repository, 
        IMapper mapper,
        ILogger<GetCarModelByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CarModelDTO> Handle(GetCarModelByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching car model with ID: {CarModelId}", request.Id);

        var carModel = await _repository.GetByIdAsync(request.Id, cancellationToken, entity => entity.Brand);
        if (carModel is null)
        {
            throw new EntityNotFoundException(nameof(CarModelEntity), request.Id);
        }
        
        _logger.LogInformation("Retrieved car model with ID: {CarModelId}", request.Id);

        return _mapper.Map<CarModelDTO>(carModel);
    }
}