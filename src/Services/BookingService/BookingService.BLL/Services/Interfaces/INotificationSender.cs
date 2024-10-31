using BookingService.Domain.Entities;

namespace BookingService.BLL.Services.Interfaces;

public interface INotificationSender
{
    Task SendAsync(NotificationEntity notification, CancellationToken cancellationToken = default);
}