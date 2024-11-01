namespace BookingService.BLL.BackgroundJobs.Interfaces;

public interface IUnsentNotificationsJob
{
    Task ProcessUnsentNotificationsAsync(CancellationToken cancellationToken = default);
}