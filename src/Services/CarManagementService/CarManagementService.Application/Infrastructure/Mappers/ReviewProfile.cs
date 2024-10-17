using AutoMapper;
using CarManagementService.Application.Helpers;
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
        
        CreateMap<PagedList<ReviewEntity>, PagedList<ReviewDTO>>()
            .ConvertUsing((src, dest, context) =>
            {
                var dtos = context.Mapper.Map<List<ReviewDTO>>(src);
                return new PagedList<ReviewDTO>(dtos, src.TotalCount, src.CurrentPage, src.PageSize);
            });
    }
}