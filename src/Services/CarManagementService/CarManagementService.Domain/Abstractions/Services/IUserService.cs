namespace CarManagementService.Domain.Abstractions.Services;

public interface IUserService
{
    Task<bool> IsUserValidAsync(Guid id, CancellationToken cancellationToken = default);
}