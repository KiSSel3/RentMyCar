using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Brand.Queries.GetBrandByName;

public class GetBrandByNameQueryHandler : IRequestHandler<GetBrandByNameQuery, BrandDTO>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBrandByNameQueryHandler> _logger;

    public GetBrandByNameQueryHandler(
        IBrandRepository repository, 
        IMapper mapper,
        ILogger<GetBrandByNameQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BrandDTO> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching brand with name: {BrandName}", request.Name);

        var brand = await _repository.GetByNameAsync(request.Name, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.Name);
        }
        
        _logger.LogInformation("Successfully retrieved brand with name: {BrandName}", request.Name);

        return _mapper.Map<BrandDTO>(brand);
    }
}