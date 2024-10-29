using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Brand.Queries.GetBrandById;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDTO>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBrandByIdQueryHandler> _logger;

    public GetBrandByIdQueryHandler(
        IBrandRepository repository, 
        IMapper mapper,
        ILogger<GetBrandByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BrandDTO> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching brand with ID: {BrandId}", request.Id);

        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.Id);
        }
        
        _logger.LogInformation("Successfully retrieved brand with ID: {BrandId}", request.Id);

        return _mapper.Map<BrandDTO>(brand);
    }
}