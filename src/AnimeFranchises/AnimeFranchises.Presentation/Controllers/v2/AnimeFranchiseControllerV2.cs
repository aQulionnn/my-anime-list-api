using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v2;
using AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries.v2;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFranchises.Presentation.Controllers.v2;

[ApiVersion("2")]
[Route("api/v{version:apiVersion}/anime-franchises")]
[ApiController]
public class AnimeFranchiseControllerV2(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeFranchiseDto createAnimeFranchiseDto)
    {
        var command = new CreateAnimeFranchiseCommandV2(createAnimeFranchiseDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAnimeFranchisesQueryV2();
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetAnimeFranchiseByIdQueryV2(id);
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeFranchiseDto updateAnimeFranchiseDto)
    {
        var command = new UpdateAnimeFranchiseCommandV2(id, updateAnimeFranchiseDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteAnimeFranchiseCommandV2(id);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}