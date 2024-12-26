namespace BookingService.BLL.Features.Notifications.BackgroundJobs.Interfaces;

public interface IUnsentNotificationsJob
{
    Task ProcessUnsentNotificationsAsync(CancellationToken cancellationToken = default);
}