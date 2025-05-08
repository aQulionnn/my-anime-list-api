using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using MediatR;

namespace FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Commands.v1;

public class UpdateFranchiseTranslationCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateFranchiseTranslationDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseInfoCommand, FranchiseTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateFranchiseTranslationDto> _validator = validator;
    
    public async Task<FranchiseTranslation> Handle(UpdateAnimeFranchiseInfoCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateFranchiseTranslationDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<FranchiseTranslation>(request.UpdateFranchiseTranslationDto);
            var result = await _unitOfWork.FranchiseTranslationRepository.UpdateAsync(request.Id, animeFranchiseInfo);
            await _unitOfWork.CommitAsync();
            
            return animeFranchiseInfo;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record UpdateAnimeFranchiseInfoCommand(Guid Id, UpdateFranchiseTranslationDto UpdateFranchiseTranslationDto)
    : IRequest<FranchiseTranslation>;