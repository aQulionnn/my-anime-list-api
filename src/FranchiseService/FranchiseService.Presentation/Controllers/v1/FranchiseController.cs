using FranchiseService.Application.Dtos.FranchiseDtos;
using Asp.Versioning;
using FranchiseService.Application.Features.FranchiseFeatures.Commands.v1;
using FranchiseService.Application.Features.FranchiseFeatures.Queries.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FranchiseService.Presentation.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/anime-franchises")]
[ApiController]
public class FranchiseController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFranchiseDto createFranchiseDto)
    {
        var command = new CreateFranchiseCommand(createFranchiseDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetFranchisesQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetFranchiseByIdQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateFranchiseDto updateFranchiseDto)
    {
        var command = new UpdateFranchiseCommand(id, updateFranchiseDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
    {
        var command = new DeleteFranchiseCommand(id);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}