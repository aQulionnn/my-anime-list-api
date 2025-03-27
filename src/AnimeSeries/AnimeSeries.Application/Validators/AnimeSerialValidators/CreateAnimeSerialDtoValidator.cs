using AnimeSeries.Application.Dtos.AnimeSerialDtos;
using FluentValidation;

namespace AnimeSeries.Application.Validators.AnimeSerialValidators;

public class CreateAnimeSerialDtoValidator : AbstractValidator<CreateAnimeSerialDto>
{
    public CreateAnimeSerialDtoValidator()
    {
        RuleFor(x => x.Season)
            .GreaterThan(0).WithMessage("Season must be greater than 0.");

        RuleFor(x => x.Part)
            .GreaterThan(0).WithMessage("Part must be greater than 0.");

        RuleFor(x => x.Episodes)
            .GreaterThan(0).WithMessage("Episodes must be greater than 0.");

        RuleFor(x => x.WatchedEpisodes)
            .GreaterThanOrEqualTo(0).WithMessage("Watched episodes cannot be negative.")
            .LessThanOrEqualTo(x => x.Episodes).WithMessage("Watched episodes cannot exceed total episodes.");

        RuleFor(x => x.Fillers)
            .GreaterThanOrEqualTo(0).WithMessage("Fillers cannot be negative.")
            .LessThanOrEqualTo(x => x.Episodes).WithMessage("Fillers cannot exceed total episodes.");

        RuleFor(x => x.ReleaseDate)
            .NotEmpty().WithMessage("Release date is required.");

        RuleFor(x => x.ViewingYear)
            .GreaterThan(1900).WithMessage("Viewing year must be a valid year.");

        RuleFor(x => x.ViewingOrder)
            .GreaterThan(0).WithMessage("Viewing order must be greater than 0.");

        RuleFor(x => x.PosterUrl)
            .NotEmpty().WithMessage("Poster URL is required.");

        RuleFor(x => x.FranchiseId)
            .NotEmpty().WithMessage("Franchise ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid franchise ID.");
    }
}
