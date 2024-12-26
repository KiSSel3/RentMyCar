using AutoMapper;
using CarManagementService.Application.UseCases.Car.Commands.CreateCar;
using CarManagementService.Application.UseCases.Car.Commands.DeleteCar;
using CarManagementService.Application.UseCases.Car.Commands.UpdateCar;
using CarManagementService.Presentation.Models.DTOs.Car;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementService.Presentation.Areas.Admin.Controllers;

[ApiController]
[Route("api/car")]
[Authorize(Policy = "AdminArea")]
public class CarController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CarController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateCarAsync([FromForm] CarRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateCarCommand>(request);
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCarAsync(Guid id, [FromForm] CarRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateCarCommand>(request);
        command.Id = id;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteCarCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}