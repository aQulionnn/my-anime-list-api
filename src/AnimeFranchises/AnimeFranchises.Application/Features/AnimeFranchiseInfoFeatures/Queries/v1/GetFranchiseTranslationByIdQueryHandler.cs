using AnimeFranchises.Application.Dtos.FranchiseTranslationDtos;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries.v1;

public class GetFranchiseTranslationByIdQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ResiliencePipelineProvider<string> resiliencePipelineProvider) 
    : IRequestHandler<GetAnimeFranchiseInfoByIdQuery, FranchiseTranslationResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<FranchiseTranslationResponseDto> Handle(GetAnimeFranchiseInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<FranchiseTranslationResponseDto>("anime-franchise-info-fallback");

        return await pipeline.ExecuteAsync(async token =>
        {
            var animeFranchiseInfo = await _unitOfWork.AnimeFranchiseInfoRepository.GetByIdAsync(request.Id);
            return _mapper.Map<FranchiseTranslationResponseDto>(animeFranchiseInfo);
        }, cancellationToken);
    }
}

public record GetAnimeFranchiseInfoByIdQuery(Guid Id) : IRequest<FranchiseTranslationResponseDto> { }