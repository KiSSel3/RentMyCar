using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Brand.DeleteBrand;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.Id);
        }

        await _brandRepository.DeleteAsync(brand, cancellationToken);
    }
}