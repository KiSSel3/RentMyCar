using MediatR;

namespace CarManagementService.Application.UseCases.Brand.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest
{
    public Guid Id { get; set; }
}