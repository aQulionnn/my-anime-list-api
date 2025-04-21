using AnimeFranchises.Application.Dtos.FranchiseDtos;
using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries.v1;

public class GetFranchisesQueryHandler
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeFranchisesQuery, IEnumerable<FranchiseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<IEnumerable<FranchiseResponseDto>> Handle(GetAnimeFranchisesQuery request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchises = await _cache.GetDataAsync<IEnumerable<FranchiseResponseDto>>("anime-franchises");
        if (cachedAnimeFranchises is not null)
        {
            return cachedAnimeFranchises;    
        }
        
        var animeFranchises = await _unitOfWork.AnimeFranchiseRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<FranchiseResponseDto>>(animeFranchises);
        await _cache.SetDataAsync("anime-franchises", result);
        
        return result;
    }
}

public record GetAnimeFranchisesQuery() : IRequest<IEnumerable<FranchiseResponseDto>> { }