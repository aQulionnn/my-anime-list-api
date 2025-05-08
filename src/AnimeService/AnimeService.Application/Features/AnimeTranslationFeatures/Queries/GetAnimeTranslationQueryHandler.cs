using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Application.Services;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialInfoFeatures.Queries;

public class GetAnimeTranslationQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeSerialInfosQuery, IEnumerable<AnimeTranslationResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<AnimeTranslationResponseDto>> Handle(GetAnimeSerialInfosQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeSerialInfos = await _cache.GetDataAsync<IEnumerable<AnimeTranslationResponseDto>>("anime-serial-infos");
        if (cachedAnimeSerialInfos is not null)
        {
            return cachedAnimeSerialInfos;    
        }
        
        var animeSerialInfos = await _unitOfWork.AnimeTranslationRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<AnimeTranslationResponseDto>>(animeSerialInfos);
        await _cache.SetDataAsync("anime-serial-infos", result);
        
        return result;
    }
}

public record GetAnimeSerialInfosQuery() : IRequest<IEnumerable<AnimeTranslationResponseDto>> { }
