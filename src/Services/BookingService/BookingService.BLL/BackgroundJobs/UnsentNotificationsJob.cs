using BookingService.BLL.Handlers.Interfaces;
using BookingService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.BackgroundJobs;

public class UnsentNotificationsJob
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
            
            foreach (var notification in unsentNotifications)
            {
                try
                {
                    await _notificationHandler.SendAndUpdateAsync(notification, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process notification {NotificationId}", notification.Id);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process unsent notifications");
        }
    }
}