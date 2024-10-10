using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetAllCarModels;

public class GetAllCarModelsQuery : IRequest<IEnumerable<CarModelDTO>>
{
}