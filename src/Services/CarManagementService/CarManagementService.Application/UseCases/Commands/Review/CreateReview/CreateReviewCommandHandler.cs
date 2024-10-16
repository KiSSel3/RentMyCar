using AutoMapper;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Review.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = _mapper.Map<ReviewEntity>(request);
        
        review.CreatedAt = DateTime.UtcNow;
        review.UpdatedAt = DateTime.UtcNow;

        await _reviewRepository.CreateAsync(review, cancellationToken);
    }
}