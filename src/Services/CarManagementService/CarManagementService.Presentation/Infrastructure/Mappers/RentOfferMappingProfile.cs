using AutoMapper;
using CarManagementService.Application.UseCases.RentOffer.Commands.AddImagesToRentOffer;
using CarManagementService.Application.UseCases.RentOffer.Commands.CreateRentOffer;
using CarManagementService.Application.UseCases.RentOffer.Commands.RemoveImagesFromRentOffer;
using CarManagementService.Application.UseCases.RentOffer.Commands.UpdateRentOffer;
using CarManagementService.Application.UseCases.RentOffer.Queries.GetRentOffers;
using CarManagementService.Application.UseCases.RentOffer.Queries.GetUserRentOffers;
using CarManagementService.Presentation.Models.DTOs.Common;
using CarManagementService.Presentation.Models.DTOs.RentOffer;

namespace CarManagementService.Presentation.Infrastructure.Mappers;

public class RentOfferMappingProfile : Profile
{
    public RentOfferMappingProfile()
    {
        CreateMap<CreateRentOfferRequestDTO, CreateRentOfferCommand>();
        CreateMap<UpdateRentOfferRequestDTO, UpdateRentOfferCommand>();
        
        CreateMap<AddImagesRequestDTO, AddImagesToRentOfferCommand>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        
        CreateMap<RemoveImagesRequestDTO, RemoveImagesFromRentOfferCommand>()
            .ForMember(dest => dest.ImageIds, opt => opt.MapFrom(src => src.ImageIds));
        
        CreateMap<RentOfferParametersRequestDTO, GetRentOffersQuery>();
        
        CreateMap<UserRentOffersRequestDTO, GetUserRentOffersQuery>();
    }
}