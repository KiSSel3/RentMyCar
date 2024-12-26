using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Review;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Review.Commands.UpdateReview;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateReviewCommandHandler> _logger;

    public UpdateReviewCommandHandler(
        IReviewRepository repository, 
        IMapper mapper,
        ILogger<UpdateReviewCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating review with ID: {ReviewId}", request.Id);

        var spec = new ReviewByIdSpecification(request.Id);
        
        var review = await _repository.FirstOrDefault(spec, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(ReviewEntity), request.Id);
        }

        _mapper.Map(request, review);
        review.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(review, cancellationToken);

        _logger.LogInformation("Review with ID: {ReviewId} successfully updated", request.Id);
    }
}