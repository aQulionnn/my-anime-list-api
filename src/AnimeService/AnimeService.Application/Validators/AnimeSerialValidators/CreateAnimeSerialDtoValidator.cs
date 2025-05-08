using AnimeService.Application.Dtos.AnimeSerialDtos;
using AnimeService.Application.Services;
using FluentValidation;

namespace AnimeService.Application.Validators.AnimeSerialValidators;

public class CreateAnimeSerialDtoValidator : AbstractValidator<CreateAnimeSerialDto>
{
    private readonly ICacheService _cache;
    
    public CreateAnimeSerialDtoValidator(ICacheService cache)
    {
        _cache = cache;
        
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
            .NotEqual(Guid.Empty).WithMessage("Invalid franchise ID.")
            .MustAsync(async (id, cancellationToken) => await AnimeFranchiseIdExists(id, cancellationToken))
                .WithMessage("Anime franchise ID does not exist.");
    }

    private async Task<bool> AnimeFranchiseIdExists(Guid animeFranchiseId, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseIds = await _cache.GetDataAsync<HashSet<Guid>>("anime-franchise-ids");
        return cachedAnimeFranchiseIds?.Contains(animeFranchiseId) ?? false;
    }
}
