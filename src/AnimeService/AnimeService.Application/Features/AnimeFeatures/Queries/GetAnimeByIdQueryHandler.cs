using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly;
using Polly.Registry;

namespace AnimeService.Application.Features.AnimeSerialFeatures.Queries;

public class GetAnimeByIdQueryHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        ResiliencePipelineProvider<string> resiliencePipelineProvider
    ) 
    : IRequestHandler<GetAnimeSerialByIdQuery, AnimeResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<AnimeResponseDto> Handle(GetAnimeSerialByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<AnimeResponseDto>("anime-serial-fallback");
        
        return await pipeline.ExecuteAsync(async token =>
        {
            var animeSerial = await _unitOfWork.AnimeRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AnimeResponseDto>(animeSerial);
            
        }, cancellationToken);
    }
}

public record GetAnimeSerialByIdQuery(Guid Id) : IRequest<AnimeResponseDto> { }
