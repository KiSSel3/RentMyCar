using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Review.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateReviewCommandHandler> _logger;

    public CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IRentOfferRepository rentOfferRepository,
        IMapper mapper,
        ILogger<CreateReviewCommandHandler> logger)
    {
        _reviewRepository = reviewRepository;
        _rentOfferRepository = rentOfferRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create a new review for rent offer with ID: {RentOfferId}", request.RentOfferId);

        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var review = _mapper.Map<ReviewEntity>(request);
        
        review.CreatedAt = DateTime.UtcNow;
        review.UpdatedAt = DateTime.UtcNow;
        
        await _reviewRepository.CreateAsync(review, cancellationToken);

        _logger.LogInformation("Successfully created a new review for rent offer with ID: {RentOfferId}", request.RentOfferId);
    }
    
    private async Task EnsureRelatedEntityExistsAsync(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var spec = new RentOfferByIdSpecification(request.RentOfferId);

        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.RentOfferId);
        }
    }
}