using Shared.Messages.Common;

namespace Shared.Messages.CarManagementService;

public class ReviewCreatedMessage : BaseMessage
{
    public int Rating { get; init; }
    public string Comment { get; set; }
}