using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseDtos;
using MediatR;
using Polly.Registry;

namespace FranchiseService.Application.Features.AnimeFranchiseFeatures.Queries.v2;

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

