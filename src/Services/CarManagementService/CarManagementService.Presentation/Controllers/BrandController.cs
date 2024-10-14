using CarManagementService.Application.UseCases.Queries.Brand.GetAllBrands;
using CarManagementService.Application.UseCases.Queries.Brand.GetBrandById;
using CarManagementService.Application.UseCases.Queries.Brand.GetBrandByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementService.Presentation.Controllers;

[ApiController]
[Route("api/brand")]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllBrandsAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetAllBrandsQuery();
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetBrandByIdQuery() { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-name/{name}")]
    public async Task<IActionResult> GetBrandByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var query = new GetBrandByNameQuery() { Name = name };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}