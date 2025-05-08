using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseDtos;
using MediatR;
using Polly.Registry;

namespace FranchiseService.Application.Features.AnimeFranchiseFeatures.Queries.v1;

public class GetFranchiseByIdQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ResiliencePipelineProvider<string> resiliencePipelineProvider) 
    : IRequestHandler<GetAnimeFranchiseByIdQuery, FranchiseResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    
    public async Task<FranchiseResponseDto> Handle(GetAnimeFranchiseByIdQuery request, CancellationToken cancellationToken)
    {
        var pipeline = _resiliencePipelineProvider.GetPipeline<FranchiseResponseDto>("anime-franchise-fallback");
        
        return await pipeline.ExecuteAsync(async token =>
        {
            var animeFranchise = await _unitOfWork.AnimeFranchiseRepository.GetByIdAsync(request.Id);
            return _mapper.Map<FranchiseResponseDto>(animeFranchise);
        }, cancellationToken);
    }
}

public record GetAnimeFranchiseByIdQuery(Guid Id) : IRequest<FranchiseResponseDto> { }