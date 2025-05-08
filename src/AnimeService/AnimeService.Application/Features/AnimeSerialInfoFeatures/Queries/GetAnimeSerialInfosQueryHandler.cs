using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Application.Services;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialInfoFeatures.Queries;

public class GetAnimeSerialInfosQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeSerialInfosQuery, IEnumerable<AnimeSerialInfoResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<AnimeSerialInfoResponseDto>> Handle(GetAnimeSerialInfosQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeSerialInfos = await _cache.GetDataAsync<IEnumerable<AnimeSerialInfoResponseDto>>("anime-serial-infos");
        if (cachedAnimeSerialInfos is not null)
        {
            return cachedAnimeSerialInfos;    
        }
        
        var animeSerialInfos = await _unitOfWork.AnimeSerialInfoRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<AnimeSerialInfoResponseDto>>(animeSerialInfos);
        await _cache.SetDataAsync("anime-serial-infos", result);
        
        return result;
    }
}

public record GetAnimeSerialInfosQuery() : IRequest<IEnumerable<AnimeSerialInfoResponseDto>> { }
