using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Brand.UpdateBrand;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IBrandRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
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
    }
}