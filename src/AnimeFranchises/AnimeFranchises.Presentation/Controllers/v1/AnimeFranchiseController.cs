using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v1;
using AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries.v1;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFranchises.Presentation.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/anime-franchises")]
[ApiController]
public class AnimeFranchiseController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeFranchiseDto createAnimeFranchiseDto)
    {
        var command = new CreateAnimeFranchiseCommand(createAnimeFranchiseDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAnimeFranchisesQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetAnimeFranchiseByIdQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeFranchiseDto updateAnimeFranchiseDto)
    {
        var command = new UpdateAnimeFranchiseCommand(id, updateAnimeFranchiseDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
    {
        var command = new DeleteAnimeFranchiseCommand(id);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}