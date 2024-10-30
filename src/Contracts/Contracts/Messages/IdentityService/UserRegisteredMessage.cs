using Contracts.Messages.Common;

namespace Contracts.Messages.IdentityService;

public class UserRegisteredMessage : BaseMessage
{
    public string Username { get; init; }
}