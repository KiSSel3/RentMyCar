using Shared.Messages.Common;

namespace Shared.Messages.IdentityService;

public class UserRoleAssignedMessage : BaseMessage
{
    public string Role { get; init; }
}