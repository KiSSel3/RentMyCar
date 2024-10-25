using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.RentOffer.Commands.CreateRentOffer;
using CarManagementService.Application.UseCases.RentOffer.Commands.UpdateRentOffer;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Infrastructure.Mappers;

public class RentOfferMappingProfile : Profile
{
    public RentOfferMappingProfile()
    {
        CreateMap<RentOfferEntity, RentOfferDTO>()
            .ForMember(dest => dest.Location,
                opt => opt.MapFrom(src => src.LocationModel));
        
        CreateMap<RentOfferEntity, RentOfferDetailDTO>()
            .ForMember(dest => dest.Location,
                opt => opt.MapFrom(src => src.LocationModel));

        CreateMap<CreateRentOfferCommand, RentOfferEntity>();
        CreateMap<UpdateRentOfferCommand, RentOfferEntity>();
    }
}