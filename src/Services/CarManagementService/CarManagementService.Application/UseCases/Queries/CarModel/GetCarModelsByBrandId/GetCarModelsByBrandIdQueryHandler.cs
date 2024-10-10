using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByBrandId;

public class GetCarModelsByBrandIdQueryHandler : IRequestHandler<GetCarModelsByBrandIdQuery, IEnumerable<CarModelDTO>>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;

    public GetCarModelsByBrandIdQueryHandler(ICarModelRepository carModelRepository, IMapper mapper)
    {
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarModelDTO>> Handle(GetCarModelsByBrandIdQuery request, CancellationToken cancellationToken)
    {
        var carModels = await _carModelRepository.GetByBrandIdAsync(request.BrandId, cancellationToken);
        
        return _mapper.Map<IEnumerable<CarModelDTO>>(carModels);
    }
}