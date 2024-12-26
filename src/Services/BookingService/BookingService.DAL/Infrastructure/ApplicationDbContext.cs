using BookingService.DAL.Infrastructure.Options;
using BookingService.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookingService.DAL.Infrastructure;

public class ApplicationDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDbOptions _options;

    public ApplicationDbContext(IOptions<MongoDbOptions> options)
    {
        _options = options.Value;
        
        var client = new MongoClient(_options.ConnectionString);
        _database = client.GetDatabase(_options.DatabaseName);
    }

    public IMongoCollection<NotificationEntity> Notifications => 
        _database.GetCollection<NotificationEntity>(_options.NotificationsCollectionName);

    public IMongoCollection<BookingEntity> Bookings => 
        _database.GetCollection<BookingEntity>(_options.BookingsCollectionName);
}