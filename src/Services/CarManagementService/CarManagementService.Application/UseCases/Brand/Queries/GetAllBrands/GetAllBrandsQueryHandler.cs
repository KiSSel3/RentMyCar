using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Brand.Queries.GetAllBrands;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandDTO>>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllBrandsQueryHandler> _logger;

    public GetAllBrandsQueryHandler(
        IBrandRepository repository, 
        IMapper mapper,
        ILogger<GetAllBrandsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BrandDTO>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all brands");

        var brands = await _repository.GetAllAsync(cancellationToken);
        
        _logger.LogInformation("Retrieved {BrandCount} brands", brands.Count());

        return _mapper.Map<IEnumerable<BrandDTO>>(brands);
    }
}