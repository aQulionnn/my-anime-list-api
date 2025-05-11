using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using FranchiseService.Application.Services;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseTranslationFeatures.Queries.v1;

public class GetFranchiseTranslationsQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetFranchiseTranslationsQuery, IEnumerable<FranchiseTranslationResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<FranchiseTranslationResponseDto>> Handle(GetFranchiseTranslationsQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseInfos = await _cache.GetDataAsync<IEnumerable<FranchiseTranslationResponseDto>>("anime-franchise-infos");
        if (cachedAnimeFranchiseInfos is not null)
        {
            return cachedAnimeFranchiseInfos;
        }
        
        var animeFranchiseInfos = await _unitOfWork.FranchiseTranslationRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<FranchiseTranslationResponseDto>>(animeFranchiseInfos);
        await _cache.SetDataAsync("anime-franchise-infos", result, TimeSpan.FromHours(1));
        
        return result;
    }
}

public record GetFranchiseTranslationsQuery() : IRequest<IEnumerable<FranchiseTranslationResponseDto>> { }