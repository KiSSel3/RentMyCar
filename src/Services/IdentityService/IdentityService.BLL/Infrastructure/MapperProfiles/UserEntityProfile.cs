using AutoMapper;
using Contracts.Protos;
using IdentityService.BLL.Models.DTOs.Requests.Auth;
using IdentityService.BLL.Models.DTOs.Responses.User;
using IdentityService.Domain.Entities;

namespace IdentityService.BLL.Infrastructure.MapperProfiles;

public class UserEntityProfile : Profile
{
    public UserEntityProfile()
    {
        CreateMap<RegisterRequestDTO, UserEntity>();
        CreateMap<UserEntity, UserResponseDTO>();
        CreateMap<UserEntity, GetUserByIdResponse>();
    }
}