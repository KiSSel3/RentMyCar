using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.CarModel.Queries.GetAllCarModels;

public class GetAllCarModelsQuery : IRequest<IEnumerable<CarModelDTO>>
{
}