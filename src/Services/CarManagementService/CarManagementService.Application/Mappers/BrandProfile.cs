using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;
using CarManagementService.Application.UseCases.Commands.Brand.UpdateBrand;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Application.Mappers;

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