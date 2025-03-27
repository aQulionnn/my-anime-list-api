using AnimeSeries.Application.Dtos.AnimeSerialInfoDtos;
using AnimeSeries.Application.Features.AnimeSerialInfoFeatures.Commands;
using AnimeSeries.Application.Features.AnimeSerialInfoFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeSeries.Presentation.Controllers;

[Route("api/anime-serial-infos")]
[ApiController]
public class AnimeSerialInfoController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimeSerialInfoDto createAnimeSerialInfoDto)
    {
        var command = new CreateAnimeSerialInfoCommand(createAnimeSerialInfoDto);
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
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateAnimeSerialInfoDto updateAnimeSerialInfoDto)
    {
        var command = new UpdateAnimeSerialInfoCommand(id, updateAnimeSerialInfoDto);
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
