using AutoMapper;
using IdentityService.BLL.Models.DTOs.Responses.Role;
using IdentityService.Domain.Entities;

namespace IdentityService.BLL.Infrastructure.MapperProfiles;

public class RoleEntityProfile : Profile
{
    public RoleEntityProfile()
    {
        CreateMap<RoleEntity, RoleResponseDTO>();
    }
}