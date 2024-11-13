using Contracts.Messages.Common;

namespace Contracts.Messages.CarManagementService;

public class ReviewCreatedMessage : BaseMessage
{
    public int Rating { get; init; }
    public string Comment { get; init; }
}