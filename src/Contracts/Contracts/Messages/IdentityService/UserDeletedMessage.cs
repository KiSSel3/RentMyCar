using Contracts.Messages.Common;

namespace Contracts.Messages.IdentityService;

public class UserDeletedMessage : BaseMessage
{
    public string Username { get; init; }
}