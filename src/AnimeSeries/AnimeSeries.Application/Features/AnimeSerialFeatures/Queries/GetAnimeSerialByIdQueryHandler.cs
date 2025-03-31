using AnimeSeries.Application.Dtos.AnimeSerialDtos;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly;
using Polly.Registry;

namespace AnimeSeries.Application.Features.AnimeSerialFeatures.Queries;

public class GetAnimeSerialByIdQueryHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        ResiliencePipelineProvider<string> resiliencePipelineProvider
    ) 
    : IRequestHandler<GetAnimeSerialByIdQuery, AnimeSerialResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<AnimeSerialResponseDto> Handle(GetAnimeSerialByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<AnimeSerialResponseDto>("anime-serial-fallback");
        
        return await pipeline.ExecuteAsync(async token =>
        {
            var animeSerial = await _unitOfWork.AnimeSerialRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AnimeSerialResponseDto>(animeSerial);
            
        }, cancellationToken);
    }
}

public record GetAnimeSerialByIdQuery(Guid Id) : IRequest<AnimeSerialResponseDto> { }
