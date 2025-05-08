using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using FluentValidation;

namespace AnimeService.Application.Validators.AnimeSerialInfoValidators;

public class UpdateAnimeSerialInfoDtoValidator : AbstractValidator<UpdateAnimeSerialInfoDto>
{
    public UpdateAnimeSerialInfoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(250).WithMessage("Title cannot exceed 250 characters.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Language must be a valid value.");
    }
}