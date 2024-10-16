using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Review;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Review.UpdateReview;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public UpdateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var spec = new ReviewByIdSpecification(request.Id);
        
        var review = await _reviewRepository.FirstOrDefault(spec, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(ReviewEntity), request.Id);
        }

        _mapper.Map(request, review);
        
        review.UpdatedAt = DateTime.UtcNow;

        await _reviewRepository.UpdateAsync(review, cancellationToken);
    }
}