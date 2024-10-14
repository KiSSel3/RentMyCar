using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Brand.GetAllBrands;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandDTO>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetAllBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BrandDTO>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<BrandDTO>>(brands);
    }
}