using AutoMapper;
using BookingService.BLL.Models.DTOs.Notification;
using BookingService.Domain.Entities;

namespace BookingService.BLL.Infrastructure.Mappers;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<NotificationEntity, NotificationDTO>();
    }
}