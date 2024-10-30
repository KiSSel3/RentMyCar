namespace Shared.Messages.Common;

public class BaseMessage
{
    public Guid UserId { get; init; }
    public DateTime CreatedAt { get; init; }
}