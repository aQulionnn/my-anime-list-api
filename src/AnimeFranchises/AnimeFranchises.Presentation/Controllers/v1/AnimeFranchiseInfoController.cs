using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands.v1;
using AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries.v1;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFranchises.Presentation.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/anime-franchise-info")]
[ApiController]
public class AnimeFranchiseInfoController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeFranchiseInfoDto createAnimeFranchiseInfoDto)
    {
        var command = new CreateAnimeFranchiseInfoCommand(createAnimeFranchiseInfoDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAnimeFranchiseInfosQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetAnimeFranchiseInfoByIdQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeFranchiseInfoDto updateAnimeFranchiseInfoDto)
    {
        var command = new UpdateAnimeFranchiseInfoCommand(id, updateAnimeFranchiseInfoDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteAnimeFranchiseInfoCommand(id);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}