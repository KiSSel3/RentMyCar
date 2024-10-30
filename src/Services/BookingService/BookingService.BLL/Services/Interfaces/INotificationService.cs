using BookingService.BLL.Models.DTOs.Notification;

namespace BookingService.BLL.Services.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<NotificationDTO>> GetUnsentNotificationsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<NotificationDTO>> GetUnsentNotificationsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<NotificationDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}