using System.Linq.Expressions;
using BookingService.Domain.Entities;
using MongoDB.Driver;

namespace BookingService.DAL.Repositories.Interfaces;

public interface IBookingRepository : IBaseRepository<BookingEntity>
{
    Task<IEnumerable<BookingEntity>> GetByFilterAsync(FilterDefinition<BookingEntity> filter,
        CancellationToken cancellationToken = default);
}