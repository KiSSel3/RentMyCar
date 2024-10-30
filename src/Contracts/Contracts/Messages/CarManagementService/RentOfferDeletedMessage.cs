using Contracts.Messages.Common;

namespace Contracts.Messages.CarManagementService;

public class RentOfferDeletedMessage : BaseMessage
{
    public string CarBrand { get; init; }
    public string CarModel { get; init; }
}