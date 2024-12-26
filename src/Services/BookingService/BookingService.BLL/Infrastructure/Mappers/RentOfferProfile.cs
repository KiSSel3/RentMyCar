using AutoMapper;
using BookingService.BLL.Models.Results;
using Contracts.Protos;

namespace BookingService.BLL.Infrastructure.Mappers;

public class RentOfferProfile : Profile
{
    public RentOfferProfile()
    {
        CreateMap<Google.Protobuf.WellKnownTypes.Timestamp, DateTime>()
            .ConvertUsing(src => src.ToDateTime());

        CreateMap<GetRentOfferByIdResponse, RentOfferResult>();
    }
}