using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetAllCarModels;

public class GetAllCarModelsQueryHandler : IRequestHandler<GetAllCarModelsQuery, IEnumerable<CarModelDTO>>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCarModelsQueryHandler(ICarModelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarModelDTO>> Handle(GetAllCarModelsQuery request, CancellationToken cancellationToken)
    {
        var carModels = await _repository.GetAllAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<CarModelDTO>>(carModels);
    }
}