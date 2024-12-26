namespace BookingService.DAL.Infrastructure.Options;

public class MongoDbOptions
{
    public const string DefaultSection = "MongoDb";
    
    public string ConnectionString { get; init; }
    public string DatabaseName { get; init; }
    public string NotificationsCollectionName { get; init; }
    public string BookingsCollectionName { get; init; }
}