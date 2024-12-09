using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using Contracts.Messages.IdentityService;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.Consumers;

public class UserDeletedConsumer : IConsumer<UserDeletedMessage>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ILogger<UserDeletedConsumer> _logger;

    public UserDeletedConsumer(
        IRentOfferRepository rentOfferRepository,
        ILogger<UserDeletedConsumer> logger)
    {
        _rentOfferRepository = rentOfferRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Received UserDeleted message. Starting removal of rent offers for user ID: {UserId}",
            message.UserId);
        
        var spec = new RentOfferByUserIdSpecification(message.UserId);
    
        var rentOffers = await _rentOfferRepository.GetBySpecificationAsync(spec);
        
        var deleteTasks = rentOffers.Select(rentOffer => _rentOfferRepository.DeleteAsync(rentOffer));
        
        await Task.WhenAll(deleteTasks);
        
        _logger.LogInformation("Successfully deleted rent offers for user ID: {UserId}",
            message.UserId);
    }
}