using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Application.Features.AnimeSerialInfoFeatures.Commands;
using AnimeService.Application.Features.AnimeSerialInfoFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeService.Presentation.Controllers;

[Route("api/anime-serial-infos")]
[ApiController]
public class AnimeTranslationController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeTranslationDto createAnimeTranslationDto)
    {
        var command = new CreateAnimeSerialInfoCommand(createAnimeTranslationDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAnimeSerialInfosQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var query = new GetAnimeSerialInfoByIdQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeTranslationDto updateAnimeTranslationDto)
    {
        var command = new UpdateAnimeSerialInfoCommand(id, updateAnimeTranslationDto);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteAnimeSerialInfoCommand(id);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
