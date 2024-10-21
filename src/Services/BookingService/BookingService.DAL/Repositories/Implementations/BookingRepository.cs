using System.Linq.Expressions;
using BookingService.DAL.Infrastructure;
using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using MongoDB.Driver;

namespace BookingService.DAL.Repositories.Implementations;

public class BookingRepository : IBookingRepository
{
    private readonly IMongoCollection<BookingEntity> _collection;

    public BookingRepository(ApplicationDbContext context)
    {
        _collection = context.Bookings;
    }

    public async Task<IEnumerable<BookingEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter => true).ToListAsync(cancellationToken);
    }

    public async Task<BookingEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateAsync(BookingEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(BookingEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(b => b.Id == entity.Id, entity,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<BookingEntity>> GetByFilterAsync(Expression<Func<BookingEntity, bool>> filterExpression,
        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filterExpression).ToListAsync(cancellationToken);
    }
}