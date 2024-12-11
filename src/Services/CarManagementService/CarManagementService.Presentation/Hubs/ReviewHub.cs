using Microsoft.AspNetCore.SignalR;

namespace CarManagementService.Presentation.Hubs;

public class ReviewHub : Hub
{
    private readonly ILogger<ReviewHub> _logger;

    public ReviewHub(ILogger<ReviewHub> logger)
    {
        _logger = logger;
    }

    public async Task SendMessage(Guid rentOfferId, object review)
    {
        _logger.LogInformation("Attempting to send a review message to group {RentOfferId}. Review: {@Review}", rentOfferId, review);
        
        await Clients.Group(rentOfferId.ToString()).SendAsync("ReceiveReview", review);
        
        _logger.LogInformation("Successfully sent review message to group {RentOfferId}.", rentOfferId);
    }

    public async Task NotifyReviewDeleted(Guid rentOfferId, Guid reviewId)
    {
        _logger.LogInformation("Notifying group {RentOfferId} about deleted review {ReviewId}.", rentOfferId, reviewId);
        
        await Clients.Group(rentOfferId.ToString()).SendAsync("ReviewDeleted", reviewId);
        
        _logger.LogInformation("Group {RentOfferId} notified about deleted review {ReviewId}.", rentOfferId, reviewId);
    }
    
    public async Task JoinGroup(string rentOfferId)
    {
        _logger.LogInformation("Connection {ConnectionId} attempting to join group {RentOfferId}.", Context.ConnectionId, rentOfferId);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, rentOfferId);
        
        _logger.LogInformation("Connection {ConnectionId} successfully joined group {RentOfferId}.", Context.ConnectionId, rentOfferId);
    }

    public async Task LeaveGroup(string rentOfferId)
    {
        _logger.LogInformation("Connection {ConnectionId} attempting to leave group {RentOfferId}.", Context.ConnectionId, rentOfferId);
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, rentOfferId);
        
        _logger.LogInformation("Connection {ConnectionId} successfully left group {RentOfferId}.", Context.ConnectionId, rentOfferId);
    }
}
