using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries;

public class GetAnimeFranchiseInfosQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeFranchiseInfosQuery, IEnumerable<AnimeFranchiseInfoResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<AnimeFranchiseInfoResponseDto>> Handle(GetAnimeFranchiseInfosQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseInfos = await _cache.GetDataAsync<IEnumerable<AnimeFranchiseInfoResponseDto>>("anime-franchise-infos");
        if (cachedAnimeFranchiseInfos is not null)
        {
            return cachedAnimeFranchiseInfos;
        }
        
        var animeFranchiseInfos = await _unitOfWork.AnimeFranchiseInfoRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<AnimeFranchiseInfoResponseDto>>(animeFranchiseInfos);
        await _cache.SetDataAsync("anime-franchise-infos", result);
        
        return result;
    }
}

public record GetAnimeFranchiseInfosQuery() : IRequest<IEnumerable<AnimeFranchiseInfoResponseDto>> { }