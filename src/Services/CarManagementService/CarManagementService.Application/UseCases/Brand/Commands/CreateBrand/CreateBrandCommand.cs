using MediatR;

namespace CarManagementService.Application.UseCases.Brand.Commands.CreateBrand;

public class CreateBrandCommand : IRequest
{
    public string Name { get; set; }
}