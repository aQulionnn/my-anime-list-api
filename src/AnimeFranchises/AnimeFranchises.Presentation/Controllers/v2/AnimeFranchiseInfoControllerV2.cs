using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands.v2;
using AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries.v2;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFranchises.Presentation.Controllers.v2;

[ApiVersion("2")]
[Route("api/v{version:apiVersion}/anime-franchise-info")]
[ApiController]
public class AnimeFranchiseInfoControllerV2(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeFranchiseInfoDto createAnimeFranchiseInfoDto)
    {
        var command = new CreateAnimeFranchiseInfoCommandV2(createAnimeFranchiseInfoDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAnimeFranchiseInfosQueryV2();
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetAnimeFranchiseInfoByIdQueryV2(id);
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeFranchiseInfoDto updateAnimeFranchiseInfoDto)
    {
        var command = new UpdateAnimeFranchiseInfoCommandV2(id, updateAnimeFranchiseInfoDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteAnimeFranchiseInfoCommandV2(id);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}
