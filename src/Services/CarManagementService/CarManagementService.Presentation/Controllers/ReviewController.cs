using AutoMapper;
using CarManagementService.Application.UseCases.Commands.Review.CreateReview;
using CarManagementService.Application.UseCases.Commands.Review.DeleteReview;
using CarManagementService.Application.UseCases.Commands.Review.UpdateReview;
using CarManagementService.Application.UseCases.Queries.Review.GetReviewById;
using CarManagementService.Application.UseCases.Queries.Review.GetReviews;
using CarManagementService.Presentation.Models.DTOs.Review;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarManagementService.Presentation.Controllers;

[ApiController]
[Route("api/review")]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ReviewController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateReviewAsync([FromForm] CreateReviewRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateReviewCommand>(request);

        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateReviewAsync(Guid id, [FromForm] UpdateReviewRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateReviewCommand>(request);
        command.Id = id;

        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteReviewAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteReviewCommand { Id = id };

        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpPost("get-reviews")]
    public async Task<IActionResult> GetReviewsAsync([FromForm] ReviewParametersRequestDTO request, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetReviewsQuery>(request);
        
        var result = await _mediator.Send(query, cancellationToken);
        
        var metadata = new
        {
            result.TotalCount,
            result.PageSize,
            result.CurrentPage,
            result.TotalPages,
            result.HasNext,
            result.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
        
        return Ok(result);
    }
    
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetReviewByIdQuery { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}