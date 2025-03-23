using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using FluentValidation;

namespace AnimeFranchises.Application.Validators.AnimeFranchiseInfoValidators;

public class CreateAnimeFranchiseInfoDtoValidator : AbstractValidator<CreateAnimeFranchiseInfoDto>
{
    public CreateAnimeFranchiseInfoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(250).WithMessage("Title cannot exceed 250 characters.");
        
        RuleFor(x => x.Language)
            .NotEmpty().WithMessage("Language is required.")
            .IsInEnum().WithMessage("Language must be a valid value.");

        RuleFor(x => x.AnimeFranchiseId)
            .NotEmpty().WithMessage("Anime franchise ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid anime franchise ID.");
    }
}