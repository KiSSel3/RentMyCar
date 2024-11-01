using BookingService.BLL.Models.Results;

namespace BookingService.BLL.External.Interfaces;

public interface IUserService
{
    Task<UserResult> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsUserValidAsync(Guid id, CancellationToken cancellationToken = default);
}