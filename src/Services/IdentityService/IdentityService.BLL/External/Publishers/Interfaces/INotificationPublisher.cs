using IdentityService.Domain.Entities;

namespace IdentityService.BLL.External.Publishers.Interfaces;

public interface INotificationPublisher
{
    Task PublishUserRegisteredMessageAsync(UserEntity user, CancellationToken cancellationToken = default);
    Task PublishUserDeletedMessageAsync(UserEntity user, CancellationToken cancellationToken = default);
    Task PublishUserRoleAssignedMessageAsync(UserEntity user, string roleName, CancellationToken cancellationToken = default);
    Task PublishUserRoleRemovedMessageAsync(UserEntity user, string roleName, CancellationToken cancellationToken = default);
}