using BookingService.Domain.Entities;

namespace BookingService.DAL.Repositories.Interfaces;

public interface INotificationRepository : IBaseRepository<NotificationEntity>
{
    Task<IEnumerable<NotificationEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<NotificationEntity>> GetUnsentNotificationsAsync(CancellationToken cancellationToken = default);
}