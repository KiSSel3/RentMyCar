using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Commands.RentOffer.CreateRentOffer;
using CarManagementService.Application.UseCases.Commands.RentOffer.UpdateRentOffer;
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
        
        CreateMap<PagedList<RentOfferEntity>, PagedList<RentOfferDTO>>()
            .ConvertUsing((src, dest, context) =>
            {
                var dtos = context.Mapper.Map<List<RentOfferDTO>>(src);
                return new PagedList<RentOfferDTO>(dtos, src.TotalCount, src.CurrentPage, src.PageSize);
            });
        
        CreateMap<PagedList<RentOfferEntity>, PagedList<RentOfferDetailDTO>>()
            .ConvertUsing((src, dest, context) =>
            {
                var dtos = context.Mapper.Map<List<RentOfferDetailDTO>>(src);
                return new PagedList<RentOfferDetailDTO>(dtos, src.TotalCount, src.CurrentPage, src.PageSize);
            });
    }
}