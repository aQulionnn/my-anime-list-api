using AnimeFranchises.Application.Dtos.FranchiseDtos;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using MediatR;
using Polly.Registry;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Queries.v2;

public class GetFranchiseByIdQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeFranchiseByIdQueryV2, Result<FranchiseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<FranchiseResponseDto>> Handle(GetAnimeFranchiseByIdQueryV2 request, CancellationToken cancellationToken)
    {
        var animeFranchise = await _unitOfWork.AnimeFranchiseRepository.GetByIdAsync(request.Id);

        if (animeFranchise == null)
        {
            return Result<FranchiseResponseDto>.Failure(Error.NotFound());
        }

        var responseDto = _mapper.Map<FranchiseResponseDto>(animeFranchise);
        return Result<FranchiseResponseDto>.Success(responseDto);
    }
}

public record GetAnimeFranchiseByIdQueryV2(Guid Id) : IRequest<Result<FranchiseResponseDto>> { }

