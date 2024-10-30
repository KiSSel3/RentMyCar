using Shared.Messages.Common;

namespace Shared.Messages.CarManagementService;

public class RentOfferDeletedMessage : BaseMessage
{
    public string CarBrand { get; init; }
    public string CarModel { get; init; }
}