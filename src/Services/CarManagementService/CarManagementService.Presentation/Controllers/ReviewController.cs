using AutoMapper;
using CarManagementService.Application.UseCases.Review.Commands.CreateReview;
using CarManagementService.Application.UseCases.Review.Commands.DeleteReview;
using CarManagementService.Application.UseCases.Review.Commands.UpdateReview;
using CarManagementService.Application.UseCases.Review.Queries.GetReviewById;
using CarManagementService.Application.UseCases.Review.Queries.GetReviews;
using CarManagementService.Presentation.Hubs;
using CarManagementService.Presentation.Models.DTOs.Review;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarManagementService.Presentation.Controllers;

[ApiController]
[Route("api/review")]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IHubContext<ReviewHub> _hubContext;

    public ReviewController(
        IMediator mediator,
        IMapper mapper,
        IHubContext<ReviewHub> hubContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateReviewCommand>(request);

        var newReview = await _mediator.Send(command, cancellationToken);

        await _hubContext.Clients.Groups(newReview.RentOfferId.ToString())
            .SendAsync("ReceiveReview", newReview, cancellationToken: cancellationToken);
        
        return NoContent();
    }

    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateReviewAsync(Guid id, [FromBody] ReviewRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateReviewCommand>(request);
        command.Id = id;

        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteReviewAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteReviewCommand { Id = id };

        var deletedReview = await _mediator.Send(command, cancellationToken);
        
        await _hubContext.Clients.Groups(deletedReview.RentOfferId.ToString())
            .SendAsync("ReviewDeleted", deletedReview.Id, cancellationToken: cancellationToken);
        
        return NoContent();
    }
    
    [HttpPost("get-reviews")]
    public async Task<IActionResult> GetReviewsAsync([FromBody] ReviewParametersRequestDTO request, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetReviewsQuery>(request);
        
        var result = await _mediator.Send(query, cancellationToken);
        
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