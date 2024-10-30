using Shared.Messages.Common;

namespace Shared.Messages.IdentityService;

public class UserDeletedMessage : BaseMessage
{
    public string Username { get; init; }
}