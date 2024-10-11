using CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;
using CarManagementService.Application.UseCases.Commands.Brand.DeleteBrand;
using CarManagementService.Application.UseCases.Queries.Brand.GetAllBrands;
using CarManagementService.Application.UseCases.Queries.Brand.GetBrandById;
using CarManagementService.Application.UseCases.Queries.Brand.GetBrandByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementService.Presentation.Areas.Admin.Controllers;

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