using AutoMapper;
using BookingService.BLL.Models.DTOs.Booking;
using BookingService.Domain.Entities;

namespace BookingService.BLL.Infrastructure.Mappers;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<CreateBookingDTO, BookingEntity>();
        CreateMap<UpdateBookingDTO, BookingEntity>();
        
        CreateMap<BookingEntity, BookingDTO>();
    }
}