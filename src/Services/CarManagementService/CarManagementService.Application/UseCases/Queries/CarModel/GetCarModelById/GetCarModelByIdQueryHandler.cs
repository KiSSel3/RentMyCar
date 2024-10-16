using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelById;

public class GetCarModelByIdQueryHandler : IRequestHandler<GetCarModelByIdQuery, CarModelDTO>
{
    private readonly ICarModelRepository _repository;
    private readonly IMapper _mapper;

    public GetCarModelByIdQueryHandler(ICarModelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CarModelDTO> Handle(GetCarModelByIdQuery request, CancellationToken cancellationToken)
    {
        var carModel = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException(nameof(CarModelEntity), request.Id);
        }
        
        return _mapper.Map<CarModelDTO>(carModel);
    }
}