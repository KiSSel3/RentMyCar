using Contracts.Messages.Common;

namespace Contracts.Messages.IdentityService;

public class UserRoleAssignedMessage : BaseMessage
{
    public string Role { get; init; }
}