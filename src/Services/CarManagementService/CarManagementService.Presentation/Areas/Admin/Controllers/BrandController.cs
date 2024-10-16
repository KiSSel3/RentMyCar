using AutoMapper;
using CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;
using CarManagementService.Application.UseCases.Commands.Brand.DeleteBrand;
using CarManagementService.Application.UseCases.Commands.Brand.UpdateBrand;
using CarManagementService.Presentation.Models.DTOs.Brand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementService.Presentation.Areas.Admin.Controllers;

[ApiController]
[Route("api/brand")]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BrandController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateBrandAsync([FromBody] BrandRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateBrandCommand>(request);
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateBrandAsync(Guid id, [FromBody] BrandRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateBrandCommand>(request);
        command.Id = id;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBrandAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteBrandCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}