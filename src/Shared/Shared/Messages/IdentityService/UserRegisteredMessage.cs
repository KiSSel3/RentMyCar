using Shared.Messages.Common;

namespace Shared.Messages.IdentityService;

public class UserRegisteredMessage : BaseMessage
{
    public string Username { get; init; }
}