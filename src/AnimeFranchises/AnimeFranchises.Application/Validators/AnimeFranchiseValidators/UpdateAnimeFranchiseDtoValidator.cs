using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using FluentValidation;

namespace AnimeFranchises.Application.Validators.AnimeFranchiseValidators;

public class UpdateAnimeFranchiseDtoValidator : AbstractValidator<UpdateAnimeFranchiseDto>
{
    public UpdateAnimeFranchiseDtoValidator()
    {
        RuleFor(x => x.ViewingOrder)
            .NotEmpty().WithMessage("Viewing order is required.")
            .GreaterThan(0).WithMessage("Viewing order must be greater than zero.");
    }
}