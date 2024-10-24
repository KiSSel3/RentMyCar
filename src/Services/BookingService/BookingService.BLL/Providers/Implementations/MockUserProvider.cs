using BookingService.BLL.Exceptions;
using BookingService.BLL.Models.Results;
using BookingService.BLL.Providers.Interfaces;

namespace BookingService.BLL.Providers.Implementations;

public class MockUserProvider : IUserProvider
{
    private readonly IEnumerable<UserResult> _userResults;

    public MockUserProvider()
    {
        _userResults = new List<UserResult>()
        {
            new UserResult()
            {
                Id = new Guid("9AB61044-85F6-4C5E-A93A-D860EFAE0CCE"),
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin",
                Email = "admin.rent.my.car@gmail.com",
                PhoneNumber = "+111111111111"
            }
        };
    }

    public Task<UserResult> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = _userResults.FirstOrDefault(ur => ur.Id == id);
        if (user is null)
        {
            throw new EntityNotFoundException("UserEntity", id);
        }
        
        return Task.FromResult(user);
    }

    public Task<bool> IsUserValidAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = _userResults.FirstOrDefault(ur => ur.Id == id);

        return Task.FromResult(user is not null);
    }
}