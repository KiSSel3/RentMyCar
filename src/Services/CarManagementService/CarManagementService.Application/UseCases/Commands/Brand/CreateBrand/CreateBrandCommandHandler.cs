using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;

    public CreateBrandCommandHandler(IBrandRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByNameAsync(request.Name, cancellationToken);
        if (brand is not null)
        {
            throw new EntityAlreadyExistsException(nameof(BrandEntity), request.Name);
        }

        brand = _mapper.Map<BrandEntity>(request);
        
        await _repository.CreateAsync(brand, cancellationToken);
    }
}