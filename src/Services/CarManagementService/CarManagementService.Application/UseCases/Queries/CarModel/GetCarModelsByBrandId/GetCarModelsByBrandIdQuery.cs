using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByBrandId;

public class GetCarModelsByBrandIdQuery : IRequest<IEnumerable<CarModelDTO>>
{
    public Guid BrandId { get; set; }
}
