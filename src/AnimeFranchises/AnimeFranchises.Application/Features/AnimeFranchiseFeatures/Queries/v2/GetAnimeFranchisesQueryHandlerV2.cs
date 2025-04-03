using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries.v2;

public class GetAnimeFranchisesQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetAnimeFranchisesQueryV2, Result<IEnumerable<AnimeFranchiseResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<Result<IEnumerable<AnimeFranchiseResponseDto>>> Handle(GetAnimeFranchisesQueryV2 request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchises = await _cache.GetDataAsync<IEnumerable<AnimeFranchiseResponseDto>>("anime-franchises");
        if (cachedAnimeFranchises is not null)
        {
            return Result<IEnumerable<AnimeFranchiseResponseDto>>.Success(cachedAnimeFranchises);    
        }
        
        var animeFranchises = await _unitOfWork.AnimeFranchiseRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<AnimeFranchiseResponseDto>>(animeFranchises);
        await _cache.SetDataAsync("anime-franchises", result);
        
        return Result<IEnumerable<AnimeFranchiseResponseDto>>.Success(result);
    }
}

public record GetAnimeFranchisesQueryV2() : IRequest<Result<IEnumerable<AnimeFranchiseResponseDto>>> { }
