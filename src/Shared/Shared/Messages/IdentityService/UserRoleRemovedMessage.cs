using Shared.Messages.Common;

namespace Shared.Messages.IdentityService;

public class UserRoleRemovedMessage : BaseMessage
{
    public string Role { get; init; }
}