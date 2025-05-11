using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseTranslationFeatures.Queries.v2;

public class GetFranchiseTranslationByIdQueryHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetFranchiseTranslationByIdQueryV2, Result<FranchiseTranslationResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<FranchiseTranslationResponseDto>> Handle(GetFranchiseTranslationByIdQueryV2 request, CancellationToken cancellationToken)
    {
        var animeFranchiseInfo = await _unitOfWork.FranchiseTranslationRepository.GetByIdAsync(request.Id);
        if (animeFranchiseInfo is null)
        {
            return Result<FranchiseTranslationResponseDto>.Failure(Error.NotFound());
        }

        var result = _mapper.Map<FranchiseTranslationResponseDto>(animeFranchiseInfo);
        return Result<FranchiseTranslationResponseDto>.Success(result);
    }
}

public record GetFranchiseTranslationByIdQueryV2(Guid Id) : IRequest<Result<FranchiseTranslationResponseDto>> { }
