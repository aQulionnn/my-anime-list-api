using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseDtos;
using MediatR;
using Polly.Registry;

namespace FranchiseService.Application.Features.FranchiseFeatures.Queries.v2;

public class GetFranchiseByIdQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetFranchiseByIdQueryV2, Result<FranchiseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<FranchiseResponseDto>> Handle(GetFranchiseByIdQueryV2 request, CancellationToken cancellationToken)
    {
        var animeFranchise = await _unitOfWork.FranchiseRepository.GetByIdAsync(request.Id);

        if (animeFranchise == null)
        {
            return Result<FranchiseResponseDto>.Failure(Error.NotFound());
        }

        var responseDto = _mapper.Map<FranchiseResponseDto>(animeFranchise);
        return Result<FranchiseResponseDto>.Success(responseDto);
    }
}

public record GetFranchiseByIdQueryV2(Guid Id) : IRequest<Result<FranchiseResponseDto>> { }

