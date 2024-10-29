using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Brand.Commands.CreateBrand;
using CarManagementService.Application.UseCases.Brand.Commands.UpdateBrand;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Infrastructure.Mappers;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandEntity, BrandDTO>();

        CreateMap<CreateBrandCommand, BrandEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateBrandCommand, BrandEntity>();
    }
}