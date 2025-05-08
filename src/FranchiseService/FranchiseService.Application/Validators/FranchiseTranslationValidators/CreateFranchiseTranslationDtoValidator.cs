using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;

namespace FranchiseService.Application.Validators.AnimeFranchiseInfoValidators;

public class CreateFranchiseTranslationDtoValidator : AbstractValidator<CreateFranchiseTranslationDto>
{
    public CreateFranchiseTranslationDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(250).WithMessage("Title cannot exceed 250 characters.");
        
        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Language must be a valid value.");

        RuleFor(x => x.AnimeFranchiseId)
            .NotEmpty().WithMessage("Anime franchise ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid anime franchise ID.");
    }
}