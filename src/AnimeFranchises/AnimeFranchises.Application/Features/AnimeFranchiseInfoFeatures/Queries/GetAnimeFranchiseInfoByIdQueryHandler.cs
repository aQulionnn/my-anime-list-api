using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries;

public class GetAnimeFranchiseInfoByIdQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ResiliencePipelineProvider<string> resiliencePipelineProvider) 
    : IRequestHandler<GetAnimeFranchiseInfoByIdQuery, AnimeFranchiseInfoResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<AnimeFranchiseInfoResponseDto> Handle(GetAnimeFranchiseInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<AnimeFranchiseInfoResponseDto>("anime-franchise-info-fallback");

        return await pipeline.ExecuteAsync(async token =>
        {
            var animeFranchiseInfo = await _unitOfWork.AnimeFranchiseInfoRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AnimeFranchiseInfoResponseDto>(animeFranchiseInfo);
        }, cancellationToken);
    }
}

public record GetAnimeFranchiseInfoByIdQuery(Guid Id) : IRequest<AnimeFranchiseInfoResponseDto> { }