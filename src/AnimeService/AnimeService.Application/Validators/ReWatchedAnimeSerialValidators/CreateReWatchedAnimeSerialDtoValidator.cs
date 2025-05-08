using AnimeService.Application.Dtos.ReWatchedAnimeSerialDtos;
using FluentValidation;

namespace AnimeService.Application.Validators.ReWatchedAnimeSerialValidators;

public class CreateReWatchedAnimeSerialDtoValidator : AbstractValidator<CreateReWatchedAnimeSerialDto>
{
    public CreateReWatchedAnimeSerialDtoValidator()
    {
        RuleFor(x => x.ReWatchedEpisodes)
            .GreaterThanOrEqualTo(0).WithMessage("Rewatched episodes must be zero or more.");

        RuleFor(x => x.ViewingOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Viewing order must be zero or more.");

        RuleFor(x => x.AnimeSerialId)
            .NotEmpty().WithMessage("Anime serial ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid anime serial ID.");
    }
}