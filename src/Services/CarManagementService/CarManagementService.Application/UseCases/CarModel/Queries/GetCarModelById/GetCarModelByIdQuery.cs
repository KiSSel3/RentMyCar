using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelById;

public class GetCarModelByIdQuery : IRequest<CarModelDTO>
{
    public Guid Id { get; set; }
}