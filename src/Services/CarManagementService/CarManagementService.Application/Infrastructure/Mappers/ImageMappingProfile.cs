using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Infrastructure.Mappers;

public class ImageMappingProfile : Profile
{
    public ImageMappingProfile()
    {
        CreateMap<ImageEntity, ImageDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));
    }
}