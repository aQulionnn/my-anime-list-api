using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries;

public class GetAnimeFranchiseInfosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeFranchiseInfosQuery, IEnumerable<AnimeFranchiseInfoResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<IEnumerable<AnimeFranchiseInfoResponseDto>> Handle(GetAnimeFranchiseInfosQuery request, CancellationToken cancellationToken)
    {
        var animeFranchiseInfos = await _unitOfWork.AnimeFranchiseInfoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AnimeFranchiseInfoResponseDto>>(animeFranchiseInfos);
    }
}

public record GetAnimeFranchiseInfosQuery() : IRequest<IEnumerable<AnimeFranchiseInfoResponseDto>> { }