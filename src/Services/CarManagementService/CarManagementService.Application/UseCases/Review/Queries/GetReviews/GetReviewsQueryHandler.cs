using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Common;
using CarManagementService.Domain.Specifications.Review;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Review.Queries.GetReviews;

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PaginatedResult<ReviewDTO>>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetReviewsQueryHandler> _logger;

    public GetReviewsQueryHandler(
        IReviewRepository repository, 
        IMapper mapper,
        ILogger<GetReviewsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<ReviewDTO>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching reviews with parameters");

        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var spec = CreateSpecification(request);

        var totalCount = await _repository.CountAsync(spec, cancellationToken);
        
        spec = spec.And(new ReviewPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        
        var reviews = await _repository.GetBySpecificationAsync(spec, cancellationToken);

        var reviewDTOs = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
    
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize.Value);

        var result = new PaginatedResult<ReviewDTO>
        {
            Collection = reviewDTOs,
            CurrentPage = request.PageNumber.Value,
            PageSize = request.PageSize.Value,
            TotalPageCount = totalPages,
            TotalItemCount = totalCount
        };
        
        _logger.LogInformation("Retrieved {TotalCount} reviews, returning page {PageNumber} with {PageSize} items", 
            totalCount, request.PageNumber.Value, reviews.Count());

        return result;
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