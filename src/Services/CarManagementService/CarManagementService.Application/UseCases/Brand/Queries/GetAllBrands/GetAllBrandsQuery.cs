using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Brand.Queries.GetAllBrands;

public class GetAllBrandsQuery : IRequest<IEnumerable<BrandDTO>>
{
}