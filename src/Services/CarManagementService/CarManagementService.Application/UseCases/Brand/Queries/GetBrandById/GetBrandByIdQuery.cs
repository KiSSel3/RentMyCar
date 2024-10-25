using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Brand.Queries.GetBrandById;

public class GetBrandByIdQuery : IRequest<BrandDTO>
{
    public Guid Id { get; set; }
}