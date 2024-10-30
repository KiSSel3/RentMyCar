using Shared.Messages.Common;

namespace Shared.Messages.CarManagementService;

public class RentOfferCreatedMessage : BaseMessage
{
    public string CarBrand { get; init; }
    public string CarModel { get; init; }
    public decimal PricePerDay { get; init; }
}