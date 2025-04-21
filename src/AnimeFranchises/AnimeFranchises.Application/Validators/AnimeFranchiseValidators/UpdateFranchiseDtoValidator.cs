using AnimeFranchises.Application.Dtos.FranchiseDtos;
using FluentValidation;

namespace AnimeFranchises.Application.Validators.AnimeFranchiseValidators;

public class UpdateFranchiseDtoValidator : AbstractValidator<UpdateFranchiseDto>
{
    public UpdateFranchiseDtoValidator()
    {
        RuleFor(x => x.ViewingOrder)
            .NotEmpty().WithMessage("Viewing order is required.")
            .GreaterThan(0).WithMessage("Viewing order must be greater than zero.");
    }
}