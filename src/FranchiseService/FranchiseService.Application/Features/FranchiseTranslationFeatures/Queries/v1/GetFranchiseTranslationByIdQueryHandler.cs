using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using MediatR;
using Polly.Registry;

namespace FranchiseService.Application.Features.FranchiseTranslationFeatures.Queries.v1;

public class GetFranchiseTranslationByIdQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ResiliencePipelineProvider<string> resiliencePipelineProvider) 
    : IRequestHandler<GetFranchiseTranslationByIdQuery, FranchiseTranslationResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<FranchiseTranslationResponseDto> Handle(GetFranchiseTranslationByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<FranchiseTranslationResponseDto>("anime-franchise-info-fallback");

        return await pipeline.ExecuteAsync(async token =>
        {
            var animeFranchiseInfo = await _unitOfWork.FranchiseTranslationRepository.GetByIdAsync(request.Id);
            return _mapper.Map<FranchiseTranslationResponseDto>(animeFranchiseInfo);
        }, cancellationToken);
    }
}

public record GetFranchiseTranslationByIdQuery(Guid Id) : IRequest<FranchiseTranslationResponseDto> { }