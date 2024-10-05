using AutoMapper;
using IdentityService.BLL.Models.DTOs.Requests.Auth;
using IdentityService.Domain.Entities;

namespace IdentityService.BLL.Infrastructure.MapperProfiles;

public class UserEntityProfile : Profile
{
    public UserEntityProfile()
    {
        CreateMap<RegisterRequestDTO, UserEntity>();
    }
}