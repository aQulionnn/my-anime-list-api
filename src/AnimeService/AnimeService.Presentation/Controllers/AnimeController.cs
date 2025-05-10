using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Application.Features.AnimeSerialFeatures.Commands;
using AnimeService.Application.Features.AnimeSerialFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeService.Presentation.Controllers;

[Route("api/anime-serials")]
[ApiController]
public class AnimeController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeDto createAnimeDto)
    {
        var command = new CreateAnimeCommand(createAnimeDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAnimeSeriesQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetAnimeSerialByIdQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeDto updateAnimeDto)
    {
        var command = new UpdateAnimeSerialCommand(id, updateAnimeDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteAnimeSerialCommand(id);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
