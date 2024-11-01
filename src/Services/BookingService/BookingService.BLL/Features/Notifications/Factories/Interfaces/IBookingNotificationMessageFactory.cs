using BookingService.Domain.Entities;
using BookingService.Domain.Enums;

namespace BookingService.BLL.Features.Notifications.Factories.Interfaces;

public interface IBookingNotificationMessageFactory
{
    NotificationEntity CreateBookingCreatedNotification(BookingEntity booking);
    NotificationEntity CreateStartReminderNotification(BookingEntity booking);
    NotificationEntity CreateEndReminderNotification(BookingEntity booking);
    NotificationEntity CreateStatusChangedNotification(BookingEntity booking, BookingStatus newStatus);
}