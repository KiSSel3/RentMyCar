using BookingService.BLL.Factories.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;

namespace BookingService.BLL.Factories.Implementations;

public class BookingNotificationMessageFactory : IBookingNotificationMessageFactory
{
    public NotificationEntity CreateBookingCreatedNotification(BookingEntity booking)
    {
        return new NotificationEntity
        {
            UserId = booking.UserId,
            CreatedAt = DateTime.UtcNow,
            Message = $"Your booking has been successfully created. " +
                      $"Rental start: {booking.RentalStart:d}, " +
                      $"end: {booking.RentalEnd:d}",
            IsSent = false
        };
    }

    public NotificationEntity CreateStartReminderNotification(BookingEntity booking) 
    {
        return new NotificationEntity
        {
            UserId = booking.UserId,
            CreatedAt = DateTime.UtcNow,
            Message = $"Reminder: your rental starts tomorrow at {booking.RentalStart:t}",
            IsSent = false
        };
    }

    public NotificationEntity CreateEndReminderNotification(BookingEntity booking)
    {
        return new NotificationEntity
        {
            UserId = booking.UserId,
            CreatedAt = DateTime.UtcNow,
            Message = $"Reminder: your rental ends tomorrow at {booking.RentalEnd:t}",
            IsSent = false
        };
    }

    public NotificationEntity CreateStatusChangedNotification(BookingEntity booking, BookingStatus newStatus)
    {
        return new NotificationEntity
        {
            UserId = booking.UserId,
            CreatedAt = DateTime.UtcNow,
            Message = $"Your booking status has been updated to {newStatus}",
            IsSent = false
        };
    }
}