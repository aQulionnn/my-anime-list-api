using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using FranchiseService.Application.Services;
using MediatR;

namespace FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Queries.v1;

public class GetFranchiseTranslationsQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeFranchiseInfosQuery, IEnumerable<FranchiseTranslationResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<FranchiseTranslationResponseDto>> Handle(GetAnimeFranchiseInfosQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseInfos = await _cache.GetDataAsync<IEnumerable<FranchiseTranslationResponseDto>>("anime-franchise-infos");
        if (cachedAnimeFranchiseInfos is not null)
        {
            return cachedAnimeFranchiseInfos;
        }
        
        var animeFranchiseInfos = await _unitOfWork.FranchiseTranslationRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<FranchiseTranslationResponseDto>>(animeFranchiseInfos);
        await _cache.SetDataAsync("anime-franchise-infos", result);
        
        return result;
    }
}

public record GetAnimeFranchiseInfosQuery() : IRequest<IEnumerable<FranchiseTranslationResponseDto>> { }