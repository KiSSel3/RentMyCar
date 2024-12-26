namespace BookingService.BLL.Models.Options;

public class SchedulerNotificationOptions
{
    public const string SectionName = "SchedulerNotification";

    public TimeSpan StartReminderOffset { get; set; }
    public TimeSpan EndReminderOffset { get; set; }
}