namespace BookingService.BLL.Models.Options;

public class GRPCOptions
{
    public const string SectionName = "GRPC";
    
    public string ConnectionStringUser { get; set; }
    public string ConnectionStringRentOffer { get; set; }
}