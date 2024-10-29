using AutoMapper;
using CarManagementService.Application.UseCases.CarModel.Queries.GetAllCarModels;
using CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelByBrandIdAndName;
using CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelById;
using CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelsByBrandId;
using CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelsByName;
using CarManagementService.Presentation.Models.DTOs.CarModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementService.Presentation.Controllers;

[ApiController]
[Route("api/car-model")]
public class CarModelController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CarModelController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllCarModelsAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetAllCarModelsQuery();
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetCarModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetCarModelByIdQuery { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-name/{name}")]
    public async Task<IActionResult> GetCarModelsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var query = new GetCarModelsByNameQuery { Name = name };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("get-by-brand-id/{id}")]
    public async Task<IActionResult> GetCarModelsByBrandIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetCarModelsByBrandIdQuery() { BrandId = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPost("get-by-parameters")]
    public async Task<IActionResult> GetCarModelByParametersAsync([FromBody] CarModelParametersRequestDTO request, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetCarModelByBrandIdAndNameQuery>(request);
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}