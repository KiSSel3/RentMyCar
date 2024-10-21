using System.Linq.Expressions;
using BookingService.Domain.Entities;

namespace BookingService.DAL.Repositories.Interfaces;

public interface IBookingRepository : IBaseRepository<BookingEntity>
{
    Task<IEnumerable<BookingEntity>> GetByFilterAsync(Expression<Func<BookingEntity, bool>> filterExpression,
        CancellationToken cancellationToken = default);
}