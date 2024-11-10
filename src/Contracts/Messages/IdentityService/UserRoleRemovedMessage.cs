using Contracts.Messages.Common;

namespace Contracts.Messages.IdentityService;

public class UserRoleRemovedMessage : BaseMessage
{
    public string Role { get; init; }
}