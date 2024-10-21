namespace BookingService.Domain.Entities;

public class NotificationEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public string Message { get; set; }
    
    public bool IsSent { get; set; }
}