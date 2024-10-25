using AutoMapper;
using CarManagementService.Application.UseCases.Review.Commands.CreateReview;
using CarManagementService.Application.UseCases.Review.Queries.GetReviews;
using CarManagementService.Presentation.Models.DTOs.Review;

namespace CarManagementService.Presentation.Infrastructure.Mappers;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<CreateReviewRequestDTO, CreateReviewCommand>();
        
        CreateMap<ReviewParametersRequestDTO, GetReviewsQuery>();
    }
}