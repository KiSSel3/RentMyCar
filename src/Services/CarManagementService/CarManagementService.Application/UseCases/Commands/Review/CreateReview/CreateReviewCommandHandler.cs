using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Review.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IRentOfferRepository rentOfferRepository,
        IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _rentOfferRepository = rentOfferRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var review = _mapper.Map<ReviewEntity>(request);
        
        review.CreatedAt = DateTime.UtcNow;
        review.UpdatedAt = DateTime.UtcNow;

        await _reviewRepository.CreateAsync(review, cancellationToken);
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