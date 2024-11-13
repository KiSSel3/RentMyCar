using BookingService.BLL.Features.Notifications.BackgroundJobs.Interfaces;
using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Features.Notifications.BackgroundJobs.Implementations;

public class UnsentNotificationsJob : IUnsentNotificationsJob
{
    private readonly INotificationHandler _notificationHandler;
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<UnsentNotificationsJob> _logger;

    public UnsentNotificationsJob(
        INotificationHandler notificationHandler,
        INotificationRepository notificationRepository,
        ILogger<UnsentNotificationsJob> logger)
    {
        _notificationHandler = notificationHandler;
        _notificationRepository = notificationRepository;
        _logger = logger;
    }
    
    public async Task ProcessUnsentNotificationsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var unsentNotifications = await _notificationRepository.GetUnsentNotificationsAsync(cancellationToken);
            
            await Parallel.ForEachAsync(unsentNotifications, cancellationToken, async (notification, ct) =>
            {
                try
                {
                    await _notificationHandler.SendAndUpdateAsync(notification, ct);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process notification {NotificationId}", notification.Id);
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process unsent notifications");
        }
    }
}