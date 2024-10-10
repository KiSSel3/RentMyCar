using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;
using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Brand.UpdateBrand;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException(nameof(BrandEntity), request.Id);
        }
        
        var existingBrand = await _brandRepository.GetByNameAsync(request.Name, cancellationToken);
        if (existingBrand is not null && existingBrand.Id != request.Id)
        {
            throw new EntityAlreadyExistsException(nameof(BrandEntity), request.Name);
        }
        
        _mapper.Map(request, brand);

        await _brandRepository.UpdateAsync(brand, cancellationToken);
        
    }
}