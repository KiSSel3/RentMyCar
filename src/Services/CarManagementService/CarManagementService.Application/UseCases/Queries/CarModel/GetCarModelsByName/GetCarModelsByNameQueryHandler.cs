using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByName;

public class GetCarModelsByNameQueryHandler : IRequestHandler<GetCarModelsByNameQuery, IEnumerable<CarModelDTO>>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;

    public GetCarModelsByNameQueryHandler(ICarModelRepository carModelRepository, IMapper mapper)
    {
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarModelDTO>> Handle(GetCarModelsByNameQuery request, CancellationToken cancellationToken)
    {
        var carModels = await _carModelRepository.GetByNameAsync(request.Name, cancellationToken);
        
        return _mapper.Map<IEnumerable<CarModelDTO>>(carModels);
    }
}