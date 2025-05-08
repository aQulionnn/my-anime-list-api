using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;

namespace FranchiseService.Application.Validators.AnimeFranchiseInfoValidators;

public class UpdateFranchiseTranslationDtoValidator : AbstractValidator<UpdateFranchiseTranslationDto>
{
    public UpdateFranchiseTranslationDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(250).WithMessage("Title cannot exceed 250 characters.");
        
        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Language must be a valid value.");
    }
}