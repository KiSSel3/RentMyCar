using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.Brand.DeleteBrand;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly IBrandRepository _repository;
    private readonly ILogger<DeleteBrandCommandHandler> _logger;

    public DeleteBrandCommandHandler(IBrandRepository repository, ILogger<DeleteBrandCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to delete brand with ID: {BrandId}", request.Id);

        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.Id);
        }

        await _repository.DeleteAsync(brand, cancellationToken);

        _logger.LogInformation("Successfully deleted brand with ID: {BrandId}", request.Id);
    }
}