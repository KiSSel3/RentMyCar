using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Review;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Review.GetReviewById;

public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDTO>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public GetReviewByIdQueryHandler(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReviewDTO> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var specification = new ReviewByIdSpecification(request.Id);

        var review = await _repository.FirstOrDefault(specification, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(ReviewEntity), request.Id);
        }

        return _mapper.Map<ReviewDTO>(review);
    }
}