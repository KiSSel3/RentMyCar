using BookingService.Domain.Entities;

namespace BookingService.DAL.Repositories.Interfaces;

public interface IBaseRepository<T>
    where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}