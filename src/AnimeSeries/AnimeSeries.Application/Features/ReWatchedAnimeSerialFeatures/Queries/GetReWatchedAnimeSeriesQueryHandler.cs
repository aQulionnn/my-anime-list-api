using AnimeSeries.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeSeries.Application.Services;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeSeries.Application.Features.ReWatchedAnimeSerialFeatures.Queries;

public class GetReWatchedAnimeSeriesQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetReWatchedAnimeSeriesQuery, IEnumerable<ReWatchedAnimeSerialResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<ReWatchedAnimeSerialResponseDto>> Handle(GetReWatchedAnimeSeriesQuery request, CancellationToken cancellationToken)
    {
        var cachedReWatchedAnimeSerials = await _cache.GetDataAsync<IEnumerable<ReWatchedAnimeSerialResponseDto>>("re-watched-anime-serials");
        if (cachedReWatchedAnimeSerials is not null)
        {
            return cachedReWatchedAnimeSerials;    
        }
        
        var reWatchedAnimeSerials = await _unitOfWork.ReWatchedAnimeSerialRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<ReWatchedAnimeSerialResponseDto>>(reWatchedAnimeSerials);
        await _cache.SetDataAsync("re-watched-anime-serials", result);
        
        return result;
    }
}

public record GetReWatchedAnimeSeriesQuery() : IRequest<IEnumerable<ReWatchedAnimeSerialResponseDto>> { }
