using AnimeService.Application.Dtos.AnimeSerialDtos;
using AnimeService.Application.Features.AnimeSerialFeatures.Commands;
using AnimeService.Application.Features.AnimeSerialFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeService.Presentation.Controllers;

[Route("api/anime-serials")]
[ApiController]
public class AnimeSerialController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeSerialDto createAnimeSerialDto)
    {
        var command = new CreateAnimeSerialCommand(createAnimeSerialDto);
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
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeSerialDto updateAnimeSerialDto)
    {
        var command = new UpdateAnimeSerialCommand(id, updateAnimeSerialDto);
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
