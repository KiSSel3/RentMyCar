using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Common;
using CarManagementService.Domain.Specifications.Review;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Review.GetReviews;

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PagedList<ReviewDTO>>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public GetReviewsQueryHandler(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedList<ReviewDTO>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var spec = CreateSpecification(request);

        var totalCount = await _repository.CountAsync(spec, cancellationToken);
        
        spec = spec.And(new ReviewPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        
        var reviews = await _repository.GetBySpecificationAsync(spec, cancellationToken);

        var pagedList = new PagedList<ReviewEntity>(reviews, totalCount, request.PageNumber.Value, request.PageSize.Value);
        
        return _mapper.Map<PagedList<ReviewDTO>>(pagedList);
    }
    
    private BaseSpecification<ReviewEntity> CreateSpecification(GetReviewsQuery request)
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

        return spec;
    }
}