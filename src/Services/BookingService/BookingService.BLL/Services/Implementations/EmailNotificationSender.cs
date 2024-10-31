using BookingService.BLL.Exceptions;
using BookingService.BLL.External.Interfaces;
using BookingService.BLL.Models.Options;
using BookingService.BLL.Services.Interfaces;
using BookingService.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BookingService.BLL.Services.Implementations;

public class EmailNotificationSender : INotificationSender
{
    private readonly EmailNotificationOptions _options;
    private readonly IUserService _userService;
    private readonly ILogger<EmailNotificationSender> _logger;
    
    public EmailNotificationSender(
        IOptions<EmailNotificationOptions> options,
        IUserService userService,
        ILogger<EmailNotificationSender> logger)
    {
        _userService = userService;
        _logger = logger;
        _options = options.Value;
    }
    
    public async Task SendAsync(NotificationEntity notification, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting to send email notification {NotificationId} for user {UserId}",
            notification.Id, notification.UserId);
        
        var user = await _userService.GetUserByIdAsync(notification.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotificationSendException($"User {notification.UserId} not found.");
        }
        
        var message = new MimeMessage
        {
            From = { new MailboxAddress("Booking Service", _options.FromEmail) },
            
            To = { new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email) },
            
            Subject = "Booking Notification",
            
            Body = new TextPart("plain")
            {
                Text = notification.Message
            }
        };

        using var client = new SmtpClient();
        
        await client.ConnectAsync(
            _options.Server, 
            _options.Port, 
            SecureSocketOptions.SslOnConnect, 
            cancellationToken);

        await client.AuthenticateAsync(
            _options.Username, 
            _options.Password, 
            cancellationToken);

        await client.SendAsync(message, cancellationToken);
            
        await client.DisconnectAsync(true, cancellationToken);
        
        _logger.LogInformation("Email notification sent successfully to {Email} for notification {NotificationId}", 
            user.Email, notification.Id);
    }
}