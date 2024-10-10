using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelByBrandIdAndName;

public class GetCarModelByBrandIdAndNameQuery : IRequest<CarModelDTO>
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
}