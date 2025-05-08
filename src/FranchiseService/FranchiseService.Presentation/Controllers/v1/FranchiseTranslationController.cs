using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Commands.v1;
using FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Queries.v1;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FranchiseService.Presentation.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/anime-franchise-info")]
[ApiController]
public class FranchiseTranslationController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFranchiseTranslationDto createFranchiseTranslationDto)
    {
        var command = new CreateAnimeFranchiseInfoCommand(createFranchiseTranslationDto);
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
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateFranchiseTranslationDto updateFranchiseTranslationDto)
    {
        var command = new UpdateAnimeFranchiseInfoCommand(id, updateFranchiseTranslationDto);
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