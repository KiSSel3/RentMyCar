using Contracts.Messages.IdentityService;
using IdentityService.BLL.External.Publishers.Interfaces;
using IdentityService.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.External.Publishers.Implementation;

public class NotificationPublisher : INotificationPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<NotificationPublisher> _logger;

    public NotificationPublisher(IPublishEndpoint publishEndpoint, ILogger<NotificationPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishUserRegisteredMessageAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing UserRegisteredMessage for user {UserId}", user.Id);
        
        var message = new UserRegisteredMessage()
        {
            UserId = user.Id,
            Username = user.UserName,
            CreatedAt = DateTime.Now
        };

        await _publishEndpoint.Publish(message, cancellationToken);
        
        _logger.LogInformation("Successfully published UserRegisteredMessage for user {UserId}", user.Id);
    }

    public async Task PublishUserDeletedMessageAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing UserDeletedMessage for user {UserId}", user.Id);
        
        var message = new UserDeletedMessage()
        {
            UserId = user.Id,
            Username = user.UserName,
            CreatedAt = DateTime.Now
        };

        await _publishEndpoint.Publish(message, cancellationToken);
        
        _logger.LogInformation("Successfully published UserDeletedMessage for user {UserId}", user.Id);
    }

    public async Task PublishUserRoleAssignedMessageAsync(UserEntity user, string roleName, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing UserRoleAssignedMessage for user {UserId} with role {RoleName}", 
            user.Id, roleName);
        
        var message = new UserRoleAssignedMessage()
        {
            UserId = user.Id,
            Role = roleName,
            CreatedAt = DateTime.Now
        };

        await _publishEndpoint.Publish(message, cancellationToken);
        
        _logger.LogInformation("Successfully published UserRoleAssignedMessage for user {UserId} with role {RoleName}", 
            user.Id, roleName);
    }

    public async Task PublishUserRoleRemovedMessageAsync(UserEntity user, string roleName, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing UserRoleRemovedMessage for user {UserId} with role {RoleName}", 
            user.Id, roleName);
        
        var message = new UserRoleRemovedMessage()
        {
            UserId = user.Id,
            Role = roleName,
            CreatedAt = DateTime.Now
        };

        await _publishEndpoint.Publish(message, cancellationToken);
        
        _logger.LogInformation("Successfully published UserRoleRemovedMessage for user {UserId} with role {RoleName}", 
            user.Id, roleName);
    }
}