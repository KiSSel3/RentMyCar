using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByName;

public class GetCarModelsByNameQueryHandler : IRequestHandler<GetCarModelsByNameQuery, IEnumerable<CarModelDTO>>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;

    public GetCarModelsByNameQueryHandler(ICarModelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarModelDTO>> Handle(GetCarModelsByNameQuery request, CancellationToken cancellationToken)
    {
        var carModels = await _repository.GetByNameAsync(request.Name, cancellationToken);
        
        return _mapper.Map<IEnumerable<CarModelDTO>>(carModels);
    }
}