using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Brand.GetBrandByName;

public class GetBrandByNameQuery : IRequest<BrandDTO>
{
    public string Name { get; set; }
}