using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries;

public class GetAnimeFranchiseInfoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeFranchiseInfoByIdQuery, AnimeFranchiseInfoResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<AnimeFranchiseInfoResponseDto> Handle(GetAnimeFranchiseInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var animeFranchiseInfo = await _unitOfWork.AnimeFranchiseInfoRepository.GetByIdAsync(request.Id);
        return _mapper.Map<AnimeFranchiseInfoResponseDto>(animeFranchiseInfo);
    }
}

public record GetAnimeFranchiseInfoByIdQuery(Guid Id) : IRequest<AnimeFranchiseInfoResponseDto> { }