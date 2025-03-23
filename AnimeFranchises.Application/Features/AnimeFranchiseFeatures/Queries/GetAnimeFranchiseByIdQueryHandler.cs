using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries;

public class GetAnimeFranchiseByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeFranchiseByIdQuery, AnimeFranchiseResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<AnimeFranchiseResponseDto> Handle(GetAnimeFranchiseByIdQuery request, CancellationToken cancellationToken)
    {
        var animeFranchise = await _unitOfWork.AnimeFranchiseRepository.GetByIdAsync(request.Id);
        return _mapper.Map<AnimeFranchiseResponseDto>(animeFranchise);
    }
}

public record GetAnimeFranchiseByIdQuery(Guid Id) : IRequest<AnimeFranchiseResponseDto> { }