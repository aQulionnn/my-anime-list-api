using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using Asp.Versioning;
using FranchiseService.Application.Features.FranchiseTranslationFeatures.Commands.v2;
using FranchiseService.Application.Features.FranchiseTranslationFeatures.Queries.v2;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FranchiseService.Presentation.Controllers.v2;

[ApiVersion("2")]
[Route("api/v{version:apiVersion}/franchise-translations")]
[ApiController]
public class FranchiseTranslationControllerV2(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFranchiseTranslationDto createFranchiseTranslationDto)
    {
        var command = new CreateFranchiseTranslationCommandV2(createFranchiseTranslationDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetFranchiseTranslationsQueryV2();
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetFranchiseTranslationByIdQueryV2(id);
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateFranchiseTranslationDto updateFranchiseTranslationDto)
    {
        var command = new UpdateFranchiseTranslationCommandV2(id, updateFranchiseTranslationDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteFranchiseTranslationCommandV2(id);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}
