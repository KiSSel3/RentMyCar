using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelByBrandIdAndName;

public class GetCarModelByBrandIdAndNameQueryHandler : IRequestHandler<GetCarModelByBrandIdAndNameQuery, CarModelDTO>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IMapper _mapper;

    public GetCarModelByBrandIdAndNameQueryHandler(ICarModelRepository carModelRepository, IMapper mapper)
    {
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task<CarModelDTO> Handle(GetCarModelByBrandIdAndNameQuery request, CancellationToken cancellationToken)
    {
        var carModel = await _carModelRepository.GetByBrandIdAndNameAsync(request.BrandId, request.Name, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException($"Car model with Brand ID {request.BrandId} and Name {request.Name} was not found.");
        }
        
        return _mapper.Map<CarModelDTO>(carModel);
    }
}