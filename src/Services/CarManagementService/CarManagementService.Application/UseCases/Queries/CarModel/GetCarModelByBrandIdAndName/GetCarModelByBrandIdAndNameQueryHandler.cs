using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelByBrandIdAndName;

public class GetCarModelByBrandIdAndNameQueryHandler : IRequestHandler<GetCarModelByBrandIdAndNameQuery, CarModelDTO>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCarModelByBrandIdAndNameQueryHandler> _logger;

    public GetCarModelByBrandIdAndNameQueryHandler(
        ICarModelRepository repository, 
        IMapper mapper,
        ILogger<GetCarModelByBrandIdAndNameQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CarModelDTO> Handle(GetCarModelByBrandIdAndNameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching car model for Brand ID: {BrandId} and Name: {Name}", request.BrandId, request.Name);

        var carModel = await _repository.GetByBrandIdAndNameAsync(request.BrandId, request.Name, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException($"Car model with Brand ID {request.BrandId} and Name {request.Name} was not found.");
        }
        
        _logger.LogInformation("Retrieved car model for Brand ID: {BrandId} and Name: {Name}", request.BrandId, request.Name);

        return _mapper.Map<CarModelDTO>(carModel);
    }
}