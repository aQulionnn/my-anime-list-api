using AnimeService.Application.Dtos.AnimeDtos;
using FluentValidation;

namespace AnimeService.Application.Validators.AnimeSerialValidators;

public class UpdateAnimeDtoValidator : AbstractValidator<UpdateAnimeDto>
{
    public UpdateAnimeDtoValidator()
    {
        RuleFor(x => x.PosterUrl)
            .NotEmpty().WithMessage("Poster URL is required.");

        RuleFor(x => x.ReleaseFormat)
            .NotEmpty().WithMessage("Release format is required.");

        RuleFor(x => x.EpisodeCount)
            .GreaterThan(0).WithMessage("Episode count must be greater than 0.");

        RuleFor(x => x.FillerCount)
            .GreaterThanOrEqualTo(0).WithMessage("Filler count cannot be negative.")
            .LessThanOrEqualTo(x => x.EpisodeCount).WithMessage("Filler count cannot exceed total episodes.");

        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero.");

        RuleFor(x => x.ReleaseDate)
            .NotEmpty().WithMessage("Release date is required.");

        RuleFor(x => x.FranchiseId)
            .NotEmpty().WithMessage("Franchise ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid franchise ID.");

        RuleFor(x => x.StudioId)
            .NotEmpty().WithMessage("Studio ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid studio ID.");
    }
}