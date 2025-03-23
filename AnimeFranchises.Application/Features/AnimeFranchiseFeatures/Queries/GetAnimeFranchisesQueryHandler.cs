using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries;

public class GetAnimeFranchisesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeFranchisesQuery, IEnumerable<AnimeFranchiseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<IEnumerable<AnimeFranchiseResponseDto>> Handle(GetAnimeFranchisesQuery request, CancellationToken cancellationToken)
    {
        var animeFranchises = await _unitOfWork.AnimeFranchiseRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AnimeFranchiseResponseDto>>(animeFranchises);
    }
}

public record GetAnimeFranchisesQuery() : IRequest<IEnumerable<AnimeFranchiseResponseDto>> { }