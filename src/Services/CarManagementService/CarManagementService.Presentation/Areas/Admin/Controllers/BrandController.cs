using CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;
using CarManagementService.Application.UseCases.Commands.Brand.DeleteBrand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementService.Presentation.Areas.Admin.Controllers;

//TODO: Add update and request dtos
[ApiController]
[Route("api/brand")]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommand command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBrand(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteBrandCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}