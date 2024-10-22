using BookingService.BLL.Models.Results;

namespace BookingService.BLL.ExternalProviders.Interfaces;

public interface IUserProvider
{
    Task<UserResult> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsUserValidAsync(Guid id, CancellationToken cancellationToken = default);
}