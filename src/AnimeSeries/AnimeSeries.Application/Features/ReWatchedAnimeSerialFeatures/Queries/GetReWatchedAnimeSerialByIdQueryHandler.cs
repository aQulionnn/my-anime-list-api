using AnimeSeries.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeSeries.Application.Features.ReWatchedAnimeSerialFeatures.Queries;

public class GetReWatchedAnimeSerialByIdQueryHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        ResiliencePipelineProvider<string> resiliencePipelineProvider
    ) 
    : IRequestHandler<GetReWatchedAnimeSerialByIdQuery, ReWatchedAnimeSerialResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<ReWatchedAnimeSerialResponseDto> Handle(GetReWatchedAnimeSerialByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<ReWatchedAnimeSerialResponseDto>("re-watched-anime-serial-fallback");

        return await pipeline.ExecuteAsync(async token =>
        {
            var reWatchedAnimeSerial = await _unitOfWork.ReWatchedAnimeSerialRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ReWatchedAnimeSerialResponseDto>(reWatchedAnimeSerial);
        }, cancellationToken);
    }
}

public record GetReWatchedAnimeSerialByIdQuery(Guid Id) : IRequest<ReWatchedAnimeSerialResponseDto> { }
