using BookingService.BLL.Exceptions;
using BookingService.BLL.External.Services.Interfaces;
using BookingService.BLL.Models.Results;

namespace BookingService.BLL.External.Services.Implementations;

public class MockUserService : IUserService
{
    private readonly IEnumerable<UserResult> _userResults;

    public MockUserService()
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
            },
            new UserResult()
            {
                Id = new Guid("ea1491fd-69d1-4041-94db-ee0295ec90c1"),
                FirstName = "Andrey",
                LastName = "Kiselev",
                UserName = "Kissel3",
                Email = "by.kissel@gmail.com",
                PhoneNumber = "+375331111111"
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