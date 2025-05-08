using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeService.Application.Features.AnimeSerialInfoFeatures.Queries;

public class GetAnimeTranslationByIdQueryHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        ResiliencePipelineProvider<string> resiliencePipelineProvider
    ) 
    : IRequestHandler<GetAnimeSerialInfoByIdQuery, AnimeTranslationResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<AnimeTranslationResponseDto> Handle(GetAnimeSerialInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<AnimeTranslationResponseDto>("anime-serial-info-fallback");

        return await pipeline.ExecuteAsync(async token =>
        {
            var animeSerialInfo = await _unitOfWork.AnimeTranslationRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AnimeTranslationResponseDto>(animeSerialInfo);
        }, cancellationToken);
    }
}

public record GetAnimeSerialInfoByIdQuery(Guid Id) : IRequest<AnimeTranslationResponseDto> { }
