using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOffers;
using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Common;
using CarManagementService.Domain.Specifications.Review;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Review.GetReviews;

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PagedList<ReviewDTO>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public GetReviewsQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<ReviewDTO>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var specification = CreateSpecification(request);

        var reviews = await _reviewRepository.GetAllAsync(specification, cancellationToken);

        var pagedList = new PagedList<ReviewEntity>(reviews, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);
        
        return _mapper.Map<PagedList<ReviewDTO>>(pagedList);
    }
    
    private ISpecification<ReviewEntity> CreateSpecification(GetReviewsQuery request)
    {
        var spec = new ReviewIncludeRentOfferSpecification() as BaseSpecification<ReviewEntity>;

        if (request.ReviewerId.HasValue)
        {
            spec = spec.And(new ReviewByReviewerIdSpecification(request.ReviewerId.Value));
        }

        if (request.RentOfferId.HasValue)
        {
            spec = spec.And(new ReviewByRentOfferIdSpecification(request.RentOfferId.Value));
        }

        if (request.MinRating.HasValue)
        {
            spec = spec.And(new ReviewByMinRatingSpecification(request.MinRating.Value));
        }

        if (request.MinDate.HasValue)
        {
            spec = spec.And(new ReviewByMinDateSpecification(request.MinDate.Value));
        }

        if (request.MaxDate.HasValue)
        {
            spec = spec.And(new ReviewByMaxDateSpecification(request.MaxDate.Value));
        }
        
        if (request.PageNumber.HasValue && request.PageSize.HasValue)
        {
            spec = spec.And(new ReviewPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        }
        
        return spec;
    }
}