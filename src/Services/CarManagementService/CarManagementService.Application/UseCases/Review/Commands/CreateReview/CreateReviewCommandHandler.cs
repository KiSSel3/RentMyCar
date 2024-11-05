using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Abstractions.Services;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;
using INotificationPublisher = CarManagementService.Application.Publishers.Interfaces.INotificationPublisher;

namespace CarManagementService.Application.UseCases.Review.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateReviewCommandHandler> _logger;
    private readonly INotificationPublisher _notificationPublisher;
    private readonly IUserService _userService;

    public CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IRentOfferRepository rentOfferRepository,
        IMapper mapper,
        ILogger<CreateReviewCommandHandler> logger,
        INotificationPublisher notificationPublisher,
        IUserService userService)
    {
        _reviewRepository = reviewRepository;
        _rentOfferRepository = rentOfferRepository;
        _mapper = mapper;
        _logger = logger;
        _notificationPublisher = notificationPublisher;
        _userService = userService;
    }

    public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create a new review for rent offer with ID: {RentOfferId}", request.RentOfferId);

        var isUserValid = await _userService.IsUserValidAsync(request.ReviewerId, cancellationToken);
        if (!isUserValid)
        {
            throw new EntityNotFoundException("UserEntity", request.ReviewerId);
        }
        
        var rentOffer = await GetRelatedRentOfferAsync(request.RentOfferId, cancellationToken);
        
        var review = _mapper.Map<ReviewEntity>(request);
        
        review.CreatedAt = DateTime.UtcNow;
        review.UpdatedAt = DateTime.UtcNow;
        
        await _reviewRepository.CreateAsync(review, cancellationToken);

        _logger.LogInformation("Successfully created a new review for rent offer with ID: {RentOfferId}", request.RentOfferId);
        
        await _notificationPublisher.PublishReviewCreatedMessageAsync(review, rentOffer, cancellationToken);
    }
    
    private async Task<RentOfferEntity> GetRelatedRentOfferAsync(Guid rentOfferId, CancellationToken cancellationToken)
    {
        var spec = new RentOfferByIdSpecification(rentOfferId);

        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), rentOfferId);
        }

        return rentOffer;
    }
}