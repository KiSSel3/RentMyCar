using BookingService.Domain.Entities;

namespace BookingService.BLL.Features.Notifications.Services.Interfaces;

public interface INotificationSender
{
    Task SendAsync(NotificationEntity notification, CancellationToken cancellationToken = default);
}