using AutoMapper;
using BookingService.BLL.Models.DTOs.Event;
using BookingService.Domain.Entities;

namespace BookingService.BLL.Infrastructure.Mappers;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventEntity, EventDTO>();
    }
}