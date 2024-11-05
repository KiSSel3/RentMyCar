using AutoMapper;
using CarManagementService.Domain.Data.Entities;
using Contracts.Protos;

namespace CarManagementService.Infrastructure.Infrastructure.Mappers;

public class RentOfferMappingProfile : Profile
{
    public RentOfferMappingProfile()
    {
        CreateMap<DateTime, Google.Protobuf.WellKnownTypes.Timestamp>()
            .ConvertUsing(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(src, DateTimeKind.Utc)));
        
        CreateMap<RentOfferEntity, GetRentOfferByIdResponse>();
    }
}