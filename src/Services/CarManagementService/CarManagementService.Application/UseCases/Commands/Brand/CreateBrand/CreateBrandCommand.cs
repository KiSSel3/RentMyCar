using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;

public class CreateBrandCommand : IRequest
{
    public string Name { get; set; }
}