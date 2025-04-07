using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Queries.v2;

public class GetAnimeFranchiseInfoByIdQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeFranchiseInfoByIdQueryV2, Result<AnimeFranchiseInfoResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<AnimeFranchiseInfoResponseDto>> Handle(GetAnimeFranchiseInfoByIdQueryV2 request, CancellationToken cancellationToken)
    {
        var animeFranchiseInfo = await _unitOfWork.AnimeFranchiseInfoRepository.GetByIdAsync(request.Id);
        if (animeFranchiseInfo is null)
        {
            return Result<AnimeFranchiseInfoResponseDto>.Failure(Error.NotFound());
        }

        var result = _mapper.Map<AnimeFranchiseInfoResponseDto>(animeFranchiseInfo);
        return Result<AnimeFranchiseInfoResponseDto>.Success(result);
    }
}

public record GetAnimeFranchiseInfoByIdQueryV2(Guid Id) : IRequest<Result<AnimeFranchiseInfoResponseDto>> { }
