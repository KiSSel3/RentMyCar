using AutoMapper;
using CarManagementService.Application.UseCases.Commands.RentOffer.AddImagesToRentOffer;
using CarManagementService.Application.UseCases.Commands.RentOffer.CreateRentOffer;
using CarManagementService.Application.UseCases.Commands.RentOffer.DeleteRentOffer;
using CarManagementService.Application.UseCases.Commands.RentOffer.RemoveImagesFromRentOffer;
using CarManagementService.Application.UseCases.Commands.RentOffer.UpdateRentOffer;
using CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferById;
using CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferDetails;
using CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOffers;
using CarManagementService.Application.UseCases.Queries.RentOffer.GetUserRentOffers;
using CarManagementService.Presentation.Models.DTOs.Common;
using CarManagementService.Presentation.Models.DTOs.RentOffer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarManagementService.Presentation.Controllers;

[ApiController]
[Route("api/rent-offer")]
public class RentOfferController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RentOfferController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRentOfferAsync([FromForm] CreateRentOfferRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateRentOfferCommand>(request);

        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateRentOfferAsync(Guid id, [FromForm] UpdateRentOfferRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateRentOfferCommand>(request);
        command.Id = id;

        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRentOfferAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteRentOfferCommand { Id = id };

        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpPost("add-images/{id}")]
    public async Task<IActionResult> AddImagesToRentOfferAsync(Guid id, [FromForm] AddImagesRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<AddImagesToRentOfferCommand>(request);
        command.RentOfferId = id;

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPost("remove-images/{id}")]
    public async Task<IActionResult> RemoveImagesFromRentOfferAsync(Guid id, [FromForm] RemoveImagesRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<RemoveImagesFromRentOfferCommand>(request);
        command.RentOfferId = id;

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
    
    [HttpPost("get-by-parameters")]
    public async Task<IActionResult> GetRentOffersByParametersAsync([FromForm] RentOfferParametersRequestDTO request, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetRentOffersQuery>(request);
        
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
    
    [HttpPost("get-by-user-id/{id}")]
    public async Task<IActionResult> GetRentOffersByUserIdAsync(Guid id, [FromForm] PaginationRequestDTO request, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetUserRentOffersQuery>(request);
        query.UserId = id;
        
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
    public async Task<IActionResult> GetRentOfferByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetRentOfferByIdQuery { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("get-details-by-id/{id}")]
    public async Task<IActionResult> GetRentOfferDetailsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetRentOfferDetailsQuery() { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}