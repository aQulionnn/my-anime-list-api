using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseDtos;
using FranchiseService.Application.Services;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseFeatures.Queries.v2;

public class GetFranchisesQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) 
    : IRequestHandler<GetFranchisesQueryV2, Result<IEnumerable<FranchiseResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;
    
    public async Task<Result<IEnumerable<FranchiseResponseDto>>> Handle(GetFranchisesQueryV2 request, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchises = await _cache.GetDataAsync<IEnumerable<FranchiseResponseDto>>("anime-franchises");
        if (cachedAnimeFranchises is not null)
        {
            return Result<IEnumerable<FranchiseResponseDto>>.Success(cachedAnimeFranchises);    
        }
        
        var animeFranchises = await _unitOfWork.FranchiseRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<FranchiseResponseDto>>(animeFranchises);
        await _cache.SetDataAsync("anime-franchises", result, TimeSpan.FromMinutes(5));
        
        return Result<IEnumerable<FranchiseResponseDto>>.Success(result);
    }
}

public record GetFranchisesQueryV2() : IRequest<Result<IEnumerable<FranchiseResponseDto>>> { }
