namespace BookingService.BLL.Models.DTOs.Notification;

public class NotificationDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public bool IsSent { get; set; }
    public DateTime CreatedAt { get; set; }
}