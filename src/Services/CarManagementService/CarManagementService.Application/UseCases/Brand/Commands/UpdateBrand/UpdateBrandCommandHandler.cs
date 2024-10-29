using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Brand.Commands.UpdateBrand;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateBrandCommandHandler> _logger;

    public UpdateBrandCommandHandler(
        IBrandRepository repository,
        IMapper mapper,
        ILogger<UpdateBrandCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to update brand with ID: {BrandId}", request.Id);

        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.Id);
        }
        
        var existingBrand = await _repository.GetByNameAsync(request.Name, cancellationToken);
        if (existingBrand is not null && existingBrand.Id != request.Id)
        {
            throw new EntityAlreadyExistsException(nameof(BrandEntity), request.Name);
        }
        
        _mapper.Map(request, brand);

        await _repository.UpdateAsync(brand, cancellationToken);

        _logger.LogInformation("Successfully updated brand with ID: {BrandId}", request.Id);
    }
}