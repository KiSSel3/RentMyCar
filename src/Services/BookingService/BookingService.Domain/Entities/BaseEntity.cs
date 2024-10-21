using MongoDB.Bson.Serialization.Attributes;

namespace BookingService.Domain.Entities;

public class BaseEntity
{
    [BsonId]
    public Guid Id { get; set; }
}