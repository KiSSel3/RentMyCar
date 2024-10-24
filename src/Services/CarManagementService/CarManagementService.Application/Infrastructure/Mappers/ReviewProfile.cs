using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Commands.Review.CreateReview;
using CarManagementService.Application.UseCases.Commands.Review.UpdateReview;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Infrastructure.Mappers;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<CreateReviewCommand, ReviewEntity>();
        CreateMap<UpdateReviewCommand, ReviewEntity>();
        
        CreateMap<ReviewEntity, ReviewDTO>();
    }
}