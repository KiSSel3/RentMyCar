using BookingService.BLL.Handlers.Interfaces;
using BookingService.BLL.Services.Interfaces;
using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Handlers.Implementations;

public class NotificationHandler : INotificationHandler
{
    private readonly INotificationRepository _repository;
    private readonly INotificationSender _sender;
    private readonly ILogger<NotificationHandler> _logger;

    public NotificationHandler(
        INotificationRepository repository,
        INotificationSender sender,
        ILogger<NotificationHandler> logger)
    {
        _repository = repository;
        _sender = sender;
        _logger = logger;
    }

    public async Task SendAndPersistAsync(NotificationEntity notification, CancellationToken cancellationToken = default)
    {
        try
        {
            await _sender.SendAsync(notification, cancellationToken);

            notification.IsSent = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Failed to send notification. UserId: {UserId}. Error: {Error}", 
                notification.UserId,
                ex.Message);
            
            notification.IsSent = false;
        }
        finally
        {
            await _repository.CreateAsync(notification, cancellationToken);
        }
    }

    public async Task SendAndUpdateAsync(NotificationEntity notification, CancellationToken cancellationToken = default)
    {
        try
        {
            await _sender.SendAsync(notification, cancellationToken);
            
            notification.IsSent = true;
            
            await _repository.UpdateAsync(notification, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Failed to send notification. UserId: {UserId}. Error: {Error}", 
                notification.UserId,
                ex.Message);
        }
    }
}