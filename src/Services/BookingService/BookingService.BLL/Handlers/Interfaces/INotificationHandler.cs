using BookingService.Domain.Entities;

namespace BookingService.BLL.Handlers.Interfaces;

public interface INotificationHandler
{
    Task SendAndPersistAsync(NotificationEntity notification, CancellationToken cancellationToken = default);
    Task SendAndUpdateAsync(NotificationEntity notification, CancellationToken cancellationToken = default);
}