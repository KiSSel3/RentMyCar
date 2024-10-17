using AutoMapper;
using CarManagementService.Application.UseCases.Commands.CarModel.CreateCarModel;
using CarManagementService.Application.UseCases.Commands.CarModel.DeleteCarModel;
using CarManagementService.Application.UseCases.Commands.CarModel.UpdateCarModel;
using CarManagementService.Presentation.Models.DTOs.CarModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementService.Presentation.Areas.Admin.Controllers;

[ApiController]
[Route("api/car-model")]
[Authorize(Policy = "AdminArea")]
public class CarModelController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CarModelController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateCarModelAsync([FromForm] CarModelRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateCarModelCommand>(request);
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCarModelAsync(Guid id, [FromForm] CarModelRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateCarModelCommand>(request);
        command.Id = id;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCarModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteCarModelCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}