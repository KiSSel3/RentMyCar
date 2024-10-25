using AutoMapper;
using CarManagementService.Application.UseCases.Queries.Car.GetCarById;
using CarManagementService.Application.UseCases.Queries.Car.GetCars;
using CarManagementService.Presentation.Models.DTOs.Car;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarManagementService.Presentation.Controllers;

[ApiController]
[Route("api/car")]
public class CarController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CarController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("get-by-parameters")]
    public async Task<IActionResult> GetCarsByParametersAsync([FromBody] CarParametersRequestDTO request, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetCarsQuery>(request);
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetCarByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetCarByIdQuery { CarId = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}