using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Brand.GetBrandByName;

public class GetBrandByNameQueryHandler : IRequestHandler<GetBrandByNameQuery, BrandDTO>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;

    public GetBrandByNameQueryHandler(IBrandRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BrandDTO> Handle(GetBrandByName.GetBrandByNameQuery request, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByNameAsync(request.Name, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.Name);
        }
        
        return _mapper.Map<BrandDTO>(brand);
    }
}