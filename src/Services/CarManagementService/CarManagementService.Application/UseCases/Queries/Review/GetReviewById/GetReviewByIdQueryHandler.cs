using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Review;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Queries.Review.GetReviewById;

public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDTO>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetReviewByIdQueryHandler> _logger;

    public GetReviewByIdQueryHandler(
        IReviewRepository repository, 
        IMapper mapper,
        ILogger<GetReviewByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReviewDTO> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching review with ID: {ReviewId}", request.Id);

        var specification = new ReviewByIdSpecification(request.Id);

        var review = await _repository.FirstOrDefault(specification, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(ReviewEntity), request.Id);
        }

        _logger.LogInformation("Retrieved review with ID: {ReviewId}", request.Id);

        return _mapper.Map<ReviewDTO>(review);
    }
}