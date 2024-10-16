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
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public GetReviewByIdQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task<ReviewDTO> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var specification = new ReviewByIdSpecification(request.Id);

        var review = await _reviewRepository.GetAllAsync(specification, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(ReviewEntity), request.Id);
        }

        return _mapper.Map<ReviewDTO>(review);
    }
}