using BookingService.BLL.Features.Notifications.BackgroundJobs.Interfaces;
using BookingService.BLL.Features.Notifications.Handlers.Interfaces;
using BookingService.BLL.Models.Options;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookingService.BLL.Features.Notifications.BackgroundJobs.Implementations;

public class BookingNotificationScheduler : IBookingNotificationScheduler
{
    private readonly SchedulerNotificationOptions _notificationOptions;
    private readonly ILogger<BookingNotificationScheduler> _logger;

    public BookingNotificationScheduler(
        IOptions<SchedulerNotificationOptions> notificationOptions,
        ILogger<BookingNotificationScheduler> logger)
    {
        _logger = logger;
        _notificationOptions = notificationOptions.Value;
    }
    
    public void ScheduleBookingCreatedNotification(BookingEntity booking)
    {
        try
        {
            var notification = new NotificationEntity
            {
                UserId = booking.UserId,
                CreatedAt = DateTime.UtcNow,
                Message = $"Your booking has been successfully created. " +
                          $"Rental start: {booking.RentalStart:d}, " +
                          $"end: {booking.RentalEnd:d}",
                IsSent = false
            };
            
            BackgroundJob.Enqueue<INotificationHandler>(
                handler => handler.SendAndPersistAsync(
                    notification, CancellationToken.None));

            _logger.LogInformation("Booking created notification scheduled for booking {BookingId}", booking.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to schedule booking created notification for booking {BookingId}", booking.Id);
        }
    }
    
    public void ScheduleStatusChangedNotification(BookingEntity booking, BookingStatus newStatus)
    {
        try
        {
            var notification = new NotificationEntity
            {
                UserId = booking.UserId,
                CreatedAt = DateTime.UtcNow,
                Message = $"Your booking status has been updated to {newStatus}",
                IsSent = false
            };

            BackgroundJob.Enqueue<INotificationHandler>(
                handler => handler.SendAndPersistAsync(
                    notification, CancellationToken.None));
            
            _logger.LogInformation("Status changed notification scheduled for booking {BookingId}", booking.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to schedule status changed notification for booking {BookingId}", booking.Id);
        }
    }

    public void ScheduleReminder(BookingEntity booking)
    {
        try
        {
            ScheduleStartReminder(booking);
            ScheduleEndReminder(booking);
            
            _logger.LogInformation("Notifications scheduled for booking {BookingId}", booking.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to schedule notifications for booking {BookingId}", booking.Id);
        }
    }
    
    private void ScheduleStartReminder(BookingEntity booking)
    {
        var reminderTime = booking.RentalStart - _notificationOptions.StartReminderOffset;
    
        if (reminderTime > DateTime.UtcNow)
        {
            var notification = new NotificationEntity
            {
                UserId = booking.UserId,
                CreatedAt = DateTime.UtcNow,
                Message = $"Reminder: your rental starts tomorrow at {booking.RentalStart:t}",
                IsSent = false
            };

            BackgroundJob.Schedule<INotificationHandler>(
                handler => handler.SendAndPersistAsync(
                    notification, 
                    CancellationToken.None),
                new DateTimeOffset(reminderTime));
        }
    }

    private void ScheduleEndReminder(BookingEntity booking)
    {
        var reminderTime = booking.RentalEnd - _notificationOptions.EndReminderOffset;
    
        if (reminderTime > DateTime.UtcNow)
        {
            var notification = new NotificationEntity
            {
                UserId = booking.UserId,
                CreatedAt = DateTime.UtcNow,
                Message = $"Reminder: your rental ends tomorrow at {booking.RentalEnd:t}",
                IsSent = false
            };

            BackgroundJob.Schedule<INotificationHandler>(
                handler => handler.SendAndPersistAsync(
                    notification, 
                    CancellationToken.None),
                new DateTimeOffset(reminderTime));
        }
    }
}