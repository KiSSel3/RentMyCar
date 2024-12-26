using MediatR;

namespace CarManagementService.Application.UseCases.Brand.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}