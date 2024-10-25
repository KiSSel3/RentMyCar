using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Brand.Queries.GetBrandByName;

public class GetBrandByNameQuery : IRequest<BrandDTO>
{
    public string Name { get; set; }
}