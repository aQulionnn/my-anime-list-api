using FranchiseService.Application.Dtos.FranchiseDtos;
using Asp.Versioning;
using FranchiseService.Application.Features.FranchiseFeatures.Commands.v2;
using FranchiseService.Application.Features.FranchiseFeatures.Queries.v2;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FranchiseService.Presentation.Controllers.v2;

[ApiVersion("2")]
[Route("api/v{version:apiVersion}/anime-franchises")]
[ApiController]
public class FranchiseControllerV2(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFranchiseDto createFranchiseDto)
    {
        var command = new CreateFranchiseCommandV2(createFranchiseDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetFranchisesQueryV2();
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetFranchiseByIdQueryV2(id);
        var result = await _sender.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateFranchiseDto updateFranchiseDto)
    {
        var command = new UpdateFranchiseCommandV2(id, updateFranchiseDto);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteFranchiseCommandV2(id);
        var result = await _sender.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}