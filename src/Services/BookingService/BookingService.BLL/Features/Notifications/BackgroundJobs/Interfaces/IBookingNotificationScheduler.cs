using BookingService.Domain.Entities;
using BookingService.Domain.Enums;

namespace BookingService.BLL.Features.Notifications.BackgroundJobs.Interfaces;

public interface IBookingNotificationScheduler
{
    void ScheduleBookingCreatedNotification(BookingEntity booking);
    void ScheduleStatusChangedNotification(BookingEntity booking, BookingStatus newStatus);
    void ScheduleReminder(BookingEntity booking);
}