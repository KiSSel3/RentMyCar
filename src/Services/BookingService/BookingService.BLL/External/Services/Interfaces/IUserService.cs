using BookingService.BLL.Models.Results;

namespace BookingService.BLL.External.Services.Interfaces;

public interface IUserService
{
    Task<UserResult> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsUserValidAsync(Guid id, CancellationToken cancellationToken = default);
}