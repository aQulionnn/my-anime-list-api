using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using MediatR;

namespace FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Commands.v2;

public class CreateFranchiseTranslationCommandHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateFranchiseTranslationDto> validator) 
    : IRequestHandler<CreateAnimeFranchiseInfoCommandV2, Result<FranchiseTranslation>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateFranchiseTranslationDto> _validator = validator;
    
    public async Task<Result<FranchiseTranslation>> Handle(CreateAnimeFranchiseInfoCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateFranchiseTranslationDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<FranchiseTranslation>.Failure(Error.ValidationFailed(validation.Errors));
        }
        
        var franchiseExists = await _unitOfWork.AnimeFranchiseRepository.GetByIdAsync(request.CreateFranchiseTranslationDto.AnimeFranchiseId);
        if (franchiseExists is null)
        {
            return Result<FranchiseTranslation>.Failure(Error.NotFound());
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<FranchiseTranslation>(request.CreateFranchiseTranslationDto);
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.CreateAsync(animeFranchiseInfo);
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

public record CreateAnimeFranchiseInfoCommandV2(CreateFranchiseTranslationDto CreateFranchiseTranslationDto) 
    : IRequest<Result<FranchiseTranslation>>;
