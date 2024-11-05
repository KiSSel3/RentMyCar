using AutoMapper;
using BookingService.BLL.Models.Results;
using Contracts.Protos;

namespace BookingService.BLL.Infrastructure.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<GetUserByIdResponse, UserResult>();
    }
}