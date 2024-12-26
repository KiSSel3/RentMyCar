using BookingService.DAL.Infrastructure;
using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using MongoDB.Driver;

namespace BookingService.DAL.Repositories.Implementations;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<NotificationEntity> _collection;

    public NotificationRepository(ApplicationDbContext context)
    {
        _collection = context.Notifications;
    }

    public async Task<IEnumerable<NotificationEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter => true).ToListAsync(cancellationToken);
    }

    public async Task<NotificationEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(n => n.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateAsync(NotificationEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(NotificationEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(n => n.Id == entity.Id, entity,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(n => n.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<NotificationEntity>> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(n => n.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<NotificationEntity>> GetUnsentNotificationsAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(n => !n.IsSent).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<NotificationEntity>> GetUnsentNotificationsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(n => n.UserId == userId && !n.IsSent).ToListAsync(cancellationToken);
    }
}