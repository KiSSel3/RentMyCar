using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Review;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Review.Commands.DeleteReview;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly IReviewRepository _repository;
    private readonly ILogger<DeleteReviewCommandHandler> _logger;

    public DeleteReviewCommandHandler(
        IReviewRepository repository,
        ILogger<DeleteReviewCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to delete review with ID: {ReviewId}", request.Id);

        var spec = new ReviewByIdSpecification(request.Id);
        
        var review = await _repository.FirstOrDefault(spec, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(ReviewEntity), request.Id);
        }
        
        await _repository.DeleteAsync(review, cancellationToken);

        _logger.LogInformation("Successfully deleted review with ID: {ReviewId}", request.Id);
    }
}