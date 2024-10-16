using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Review;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Review.DeleteReview;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;

    public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var spec = new ReviewByIdSpecification(request.Id);
        
        var review = await _reviewRepository.FirstOrDefault(spec, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(ReviewEntity), request.Id);
        }

        await _reviewRepository.DeleteAsync(review, cancellationToken);
    }
}