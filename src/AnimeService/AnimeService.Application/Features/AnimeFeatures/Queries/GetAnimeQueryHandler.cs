using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Application.Services;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialFeatures.Queries;

public class GetAnimeQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeSeriesQuery, IEnumerable<AnimeResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<AnimeResponseDto>> Handle(GetAnimeSeriesQuery request, CancellationToken cancellationToken)
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

public record GetAnimeSeriesQuery() : IRequest<IEnumerable<AnimeResponseDto>> { }
