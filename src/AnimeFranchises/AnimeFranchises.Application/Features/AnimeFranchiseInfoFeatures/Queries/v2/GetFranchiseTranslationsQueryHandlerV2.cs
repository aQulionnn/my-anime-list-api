using AnimeFranchises.Application.Dtos.FranchiseTranslationDtos;
using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries.v2;

public class GetFranchiseTranslationsQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeFranchiseInfosQueryV2, Result<IEnumerable<FranchiseTranslationResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;

    public async Task<Result<IEnumerable<FranchiseTranslationResponseDto>>> Handle(GetAnimeFranchiseInfosQueryV2 request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseInfos = await _cache.GetDataAsync<IEnumerable<FranchiseTranslationResponseDto>>("anime-franchise-infos");
        if (cachedAnimeFranchiseInfos is not null)
        {
            return Result<IEnumerable<FranchiseTranslationResponseDto>>.Success(cachedAnimeFranchiseInfos);
        }
        
        var animeFranchiseInfos = await _unitOfWork.AnimeFranchiseInfoRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<FranchiseTranslationResponseDto>>(animeFranchiseInfos);
        await _cache.SetDataAsync("anime-franchise-infos", result);

        return Result<IEnumerable<FranchiseTranslationResponseDto>>.Success(result);
    }
}

public record GetAnimeFranchiseInfosQueryV2() : IRequest<Result<IEnumerable<FranchiseTranslationResponseDto>>> { }
