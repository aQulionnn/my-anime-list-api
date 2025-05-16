using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Application.Services;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeService.Application.Features.AnimeFeatures.Queries;

internal sealed class GetAnimeQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeQuery, IEnumerable<AnimeResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<AnimeResponseDto>> Handle(GetAnimeQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeSerials = await _cache.GetDataAsync<IEnumerable<AnimeResponseDto>>("anime-series");
        if (cachedAnimeSerials is not null)
        {
            return cachedAnimeSerials;    
        }
        
        var animeSerials = await _unitOfWork.AnimeRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<AnimeResponseDto>>(animeSerials);
        await _cache.SetDataAsync("anime-serials", result);
        
        return result;
    }
}

public record GetAnimeQuery() : IRequest<IEnumerable<AnimeResponseDto>> { }
