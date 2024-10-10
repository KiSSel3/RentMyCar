using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Brand.DeleteBrand;

public class DeleteBrandCommand : IRequest
{
    public Guid Id { get; set; }
}