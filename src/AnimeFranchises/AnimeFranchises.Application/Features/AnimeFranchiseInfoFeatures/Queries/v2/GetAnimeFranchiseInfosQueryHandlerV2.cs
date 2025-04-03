using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries.v2;

public class GetAnimeFranchiseInfosQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeFranchiseInfosQueryV2, Result<IEnumerable<AnimeFranchiseInfoResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;

    public async Task<Result<IEnumerable<AnimeFranchiseInfoResponseDto>>> Handle(GetAnimeFranchiseInfosQueryV2 request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseInfos = await _cache.GetDataAsync<IEnumerable<AnimeFranchiseInfoResponseDto>>("anime-franchise-infos");
        if (cachedAnimeFranchiseInfos is not null)
        {
            return Result<IEnumerable<AnimeFranchiseInfoResponseDto>>.Success(cachedAnimeFranchiseInfos);
        }
        
        var animeFranchiseInfos = await _unitOfWork.AnimeFranchiseInfoRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<AnimeFranchiseInfoResponseDto>>(animeFranchiseInfos);
        await _cache.SetDataAsync("anime-franchise-infos", result);

        return Result<IEnumerable<AnimeFranchiseInfoResponseDto>>.Success(result);
    }
}

public record GetAnimeFranchiseInfosQueryV2() : IRequest<Result<IEnumerable<AnimeFranchiseInfoResponseDto>>> { }
