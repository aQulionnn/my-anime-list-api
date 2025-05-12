using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using FranchiseService.Application.Services;
using MediatR;
using SharedKernel.Shared;

namespace FranchiseService.Application.Features.FranchiseTranslationFeatures.Queries.v2;

public class GetFranchiseTranslationsQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetFranchiseTranslationsQueryV2, Result<IEnumerable<FranchiseTranslationResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;

    public async Task<Result<IEnumerable<FranchiseTranslationResponseDto>>> Handle(GetFranchiseTranslationsQueryV2 request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseInfos = await _cache.GetDataAsync<IEnumerable<FranchiseTranslationResponseDto>>("anime-franchise-infos");
        if (cachedAnimeFranchiseInfos is not null)
        {
            return Result<IEnumerable<FranchiseTranslationResponseDto>>.Success(cachedAnimeFranchiseInfos);
        }
        
        var animeFranchiseInfos = await _unitOfWork.FranchiseTranslationRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<FranchiseTranslationResponseDto>>(animeFranchiseInfos);
        await _cache.SetDataAsync("anime-franchise-infos", result, TimeSpan.FromHours(1));

        return Result<IEnumerable<FranchiseTranslationResponseDto>>.Success(result);
    }
}

public record GetFranchiseTranslationsQueryV2() : IRequest<Result<IEnumerable<FranchiseTranslationResponseDto>>> { }
