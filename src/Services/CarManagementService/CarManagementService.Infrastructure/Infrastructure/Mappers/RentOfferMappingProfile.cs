using AutoMapper;
using CarManagementService.Domain.Data.Entities;
using Contracts.Protos;

namespace CarManagementService.Infrastructure.Infrastructure.Mappers;

public class RentOfferMappingProfile : Profile
{
    public RentOfferMappingProfile()
    {
        CreateMap<RentOfferEntity, GetRentOfferByIdResponse>();
    }
}