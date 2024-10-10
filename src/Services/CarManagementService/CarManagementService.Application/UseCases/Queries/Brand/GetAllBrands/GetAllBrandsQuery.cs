using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Brand.GetAllBrands;

public class GetAllBrandsQuery : IRequest<IEnumerable<BrandDTO>>
{
}