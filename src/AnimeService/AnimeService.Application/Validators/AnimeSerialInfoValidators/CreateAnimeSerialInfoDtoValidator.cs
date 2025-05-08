using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using FluentValidation;

namespace AnimeService.Application.Validators.AnimeSerialInfoValidators;

public class CreateAnimeSerialInfoDtoValidator : AbstractValidator<CreateAnimeSerialInfoDto>
{
    public CreateAnimeSerialInfoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(250).WithMessage("Title cannot exceed 250 characters.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Language must be a valid value.");

        RuleFor(x => x.AnimeSerialId)
            .NotEmpty().WithMessage("Anime serial ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid anime serial ID.");
    }
}