using AutoMapper;
using IdentityService.BLL.Models.DTOs.Requests.Role;
using IdentityService.BLL.Models.DTOs.Responses.Role;
using IdentityService.Domain.Entities;

namespace IdentityService.BLL.Infrastructure.MapperProfiles;

public class RoleEntityProfile : Profile
{
    public RoleEntityProfile()
    {
        CreateMap<RoleRequestDTO, RoleEntity>();
        CreateMap<RoleEntity, RoleResponseDTO>();
    }
}