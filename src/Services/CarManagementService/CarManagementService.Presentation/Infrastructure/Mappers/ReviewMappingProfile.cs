using AutoMapper;
using CarManagementService.Application.UseCases.Commands.Review.CreateReview;
using CarManagementService.Application.UseCases.Commands.Review.UpdateReview;
using CarManagementService.Application.UseCases.Queries.Review.GetReviews;
using CarManagementService.Presentation.Models.DTOs.Review;

namespace CarManagementService.Presentation.Infrastructure.Mappers;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<CreateReviewRequestDTO, CreateReviewCommand>();
        CreateMap<UpdateReviewRequestDTO, UpdateReviewCommand>();

        CreateMap<ReviewParametersRequestDTO, GetReviewsQuery>();
    }
}