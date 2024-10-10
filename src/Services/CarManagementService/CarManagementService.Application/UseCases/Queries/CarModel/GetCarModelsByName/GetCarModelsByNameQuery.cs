using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByName;

public class GetCarModelsByNameQuery : IRequest<IEnumerable<CarModelDTO>>
{
    public string Name { get; set; }
}