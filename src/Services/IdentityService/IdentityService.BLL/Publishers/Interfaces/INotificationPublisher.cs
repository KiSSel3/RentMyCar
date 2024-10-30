using IdentityService.Domain.Entities;

namespace IdentityService.BLL.Publishers.Interfaces;

public interface INotificationPublisher
{
    Task PublishUserRegisteredMessage(UserEntity user, CancellationToken cancellationToken = default);
    Task PublishUserDeletedMessage(UserEntity user, CancellationToken cancellationToken = default);
    Task PublishUserRoleAssignedMessage(UserEntity user, string roleName, CancellationToken cancellationToken = default);
    Task PublishUserRoleRemovedMessage(UserEntity user, string roleName, CancellationToken cancellationToken = default);
}