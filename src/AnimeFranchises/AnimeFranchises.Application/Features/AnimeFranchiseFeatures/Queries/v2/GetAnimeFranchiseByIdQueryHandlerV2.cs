using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries.v2;

public class GetAnimeFranchiseByIdQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeFranchiseByIdQueryV2, Result<AnimeFranchiseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<AnimeFranchiseResponseDto>> Handle(GetAnimeFranchiseByIdQueryV2 request, CancellationToken cancellationToken)
    {
        var animeFranchise = await _unitOfWork.AnimeFranchiseRepository.GetByIdAsync(request.Id);

        if (animeFranchise == null)
        {
            return Result<AnimeFranchiseResponseDto>.Failure(Error.NotFound());
        }

        var responseDto = _mapper.Map<AnimeFranchiseResponseDto>(animeFranchise);
        return Result<AnimeFranchiseResponseDto>.Success(responseDto);
    }
}

public record GetAnimeFranchiseByIdQueryV2(Guid Id) : IRequest<Result<AnimeFranchiseResponseDto>> { }

