using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseTranslationFeatures.Commands.v1;

internal sealed class CreateFranchiseTranslationCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateFranchiseTranslationDto> validator) 
    : IRequestHandler<CreateFranchiseTranslationCommand, FranchiseTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateFranchiseTranslationDto> _validator = validator;
    
    public async Task<FranchiseTranslation> Handle(CreateFranchiseTranslationCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateFranchiseTranslationDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }
        
        var franchiseExists = await _unitOfWork.FranchiseRepository.GetByIdAsync(request.CreateFranchiseTranslationDto.AnimeFranchiseId);
        if (franchiseExists is null)
        {
            throw new KeyNotFoundException("Anime franchise not found");
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<FranchiseTranslation>(request.CreateFranchiseTranslationDto);
            var result = await _unitOfWork.FranchiseTranslationRepository.CreateAsync(animeFranchiseInfo);
            await _unitOfWork.CommitAsync();
            
            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record CreateFranchiseTranslationCommand(CreateFranchiseTranslationDto CreateFranchiseTranslationDto) 
    : IRequest<FranchiseTranslation>;