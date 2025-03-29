using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries;

public class GetAnimeFranchiseByIdQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ResiliencePipelineProvider<string> resiliencePipelineProvider) 
    : IRequestHandler<GetAnimeFranchiseByIdQuery, AnimeFranchiseResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<AnimeFranchiseResponseDto> Handle(GetAnimeFranchiseByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<AnimeFranchiseResponseDto>("anime-franchise-fallback");
        
        return await pipeline.ExecuteAsync(async token =>
        {
            var animeFranchise = await _unitOfWork.AnimeFranchiseRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AnimeFranchiseResponseDto>(animeFranchise);
        }, cancellationToken);
    }
}

public record GetAnimeFranchiseByIdQuery(Guid Id) : IRequest<AnimeFranchiseResponseDto> { }