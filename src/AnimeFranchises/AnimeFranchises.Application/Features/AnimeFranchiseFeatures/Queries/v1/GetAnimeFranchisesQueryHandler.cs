using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries.v1;

public class GetAnimeFranchisesQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeFranchisesQuery, IEnumerable<AnimeFranchiseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<AnimeFranchiseResponseDto>> Handle(GetAnimeFranchisesQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchises = await _cache.GetDataAsync<IEnumerable<AnimeFranchiseResponseDto>>("anime-franchises");
        if (cachedAnimeFranchises is not null)
        {
            return cachedAnimeFranchises;    
        }
        
        var animeFranchises = await _unitOfWork.AnimeFranchiseRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<AnimeFranchiseResponseDto>>(animeFranchises);
        await _cache.SetDataAsync("anime-franchises", result);
        
        return result;
    }
}

public record GetAnimeFranchisesQuery() : IRequest<IEnumerable<AnimeFranchiseResponseDto>> { }