using AnimeService.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeService.Application.Features.ReWatchedAnimeSerialFeatures.Commands;
using AnimeService.Application.Features.ReWatchedAnimeSerialFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeService.Presentation.Controllers;

[Route("api/re-watched-anime-serials")]
[ApiController]
public class ReWatchedAnimeSerialController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateReWatchedAnimeSerialDto createReWatchedAnimeSerialDto)
    {
        var command = new CreateReWatchedAnimeSerialCommand(createReWatchedAnimeSerialDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetReWatchedAnimeSeriesQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetReWatchedAnimeSerialByIdQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateReWatchedAnimeSerialDto updateReWatchedAnimeSerialDto)
    {
        var command = new UpdateReWatchedAnimeSerialCommand(id, updateReWatchedAnimeSerialDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteReWatchedAnimeSerialCommand(id);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
