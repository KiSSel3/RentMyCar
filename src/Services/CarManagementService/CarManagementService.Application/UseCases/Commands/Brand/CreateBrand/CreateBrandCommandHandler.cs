using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBrandCommandHandler> _logger;

    public CreateBrandCommandHandler(
        IBrandRepository repository,
        IMapper mapper,
        ILogger<CreateBrandCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create a new brand with name: {BrandName}", request.Name);

        var brand = await _repository.GetByNameAsync(request.Name, cancellationToken);
        if (brand is not null)
        {
            throw new EntityAlreadyExistsException(nameof(BrandEntity), request.Name);
        }

        brand = _mapper.Map<BrandEntity>(request);
        
        await _repository.CreateAsync(brand, cancellationToken);

        _logger.LogInformation("Successfully created a new brand with name: {BrandName}", request.Name);
    }
}