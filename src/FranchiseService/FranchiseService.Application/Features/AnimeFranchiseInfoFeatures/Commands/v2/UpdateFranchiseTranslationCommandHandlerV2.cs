using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using MediatR;

namespace FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Commands.v2;

public class UpdateFranchiseTranslationCommandHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateFranchiseTranslationDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseInfoCommandV2, Result<FranchiseTranslation>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateFranchiseTranslationDto> _validator = validator;
    
    public async Task<Result<FranchiseTranslation>> Handle(UpdateAnimeFranchiseInfoCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateFranchiseTranslationDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<FranchiseTranslation>.Failure(Error.ValidationFailed(validation.Errors));
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<FranchiseTranslation>(request.UpdateFranchiseTranslationDto);
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.UpdateAsync(request.Id, animeFranchiseInfo);
            await _unitOfWork.CommitAsync();
            
            return Result<FranchiseTranslation>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<FranchiseTranslation>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record UpdateAnimeFranchiseInfoCommandV2(Guid Id, UpdateFranchiseTranslationDto UpdateFranchiseTranslationDto)
    : IRequest<Result<FranchiseTranslation>>;
