using BookingService.Domain.Entities;
using BookingService.Domain.Enums;

namespace BookingService.BLL.BackgroundJobs.Interfaces;

public interface IBookingNotificationScheduler
{
    void ScheduleBookingCreatedNotification(BookingEntity booking);
    void ScheduleStatusChangedNotification(BookingEntity booking, BookingStatus newStatus);
    void ScheduleReminder(BookingEntity booking);
}