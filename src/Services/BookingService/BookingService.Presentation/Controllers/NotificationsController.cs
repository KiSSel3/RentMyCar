using BookingService.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminArea")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("get-by-id/{notificationId}")]
    public async Task<ActionResult> GetNotificationById(Guid notificationId, CancellationToken cancellationToken)
    {
        var notification = await _notificationService.GetByIdAsync(notificationId, cancellationToken);
        return Ok(notification);
    }

    [HttpGet("get-by-user-id/{userId}")]
    public async Task<ActionResult> GetNotificationsByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var notifications = await _notificationService.GetByUserIdAsync(userId, cancellationToken);
        return Ok(notifications);
    }

    [HttpGet("get-unsent")]
    public async Task<ActionResult> GetUnsentNotifications(CancellationToken cancellationToken)
    {
        var notifications = await _notificationService.GetUnsentNotificationsAsync(cancellationToken);
        return Ok(notifications);
    }

    [HttpGet("get-unsent-by-user-id/{userId}")]
    public async Task<ActionResult> GetUnsentNotificationsByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var notifications = await _notificationService.GetUnsentNotificationsByUserIdAsync(userId, cancellationToken);
        return Ok(notifications);
    }
}