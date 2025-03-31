using AnimeSeries.Application.Dtos.AnimeSerialInfoDtos;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeSeries.Application.Features.AnimeSerialInfoFeatures.Queries;

public class GetAnimeSerialInfoByIdQueryHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        ResiliencePipelineProvider<string> resiliencePipelineProvider
    ) 
    : IRequestHandler<GetAnimeSerialInfoByIdQuery, AnimeSerialInfoResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<AnimeSerialInfoResponseDto> Handle(GetAnimeSerialInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<AnimeSerialInfoResponseDto>("anime-serial-info-fallback");

        return await pipeline.ExecuteAsync(async token =>
        {
            var animeSerialInfo = await _unitOfWork.AnimeSerialInfoRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AnimeSerialInfoResponseDto>(animeSerialInfo);
        }, cancellationToken);
    }
}

public record GetAnimeSerialInfoByIdQuery(Guid Id) : IRequest<AnimeSerialInfoResponseDto> { }
