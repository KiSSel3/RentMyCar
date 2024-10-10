using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Brand.UpdateBrand;

public class UpdateBrandCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}