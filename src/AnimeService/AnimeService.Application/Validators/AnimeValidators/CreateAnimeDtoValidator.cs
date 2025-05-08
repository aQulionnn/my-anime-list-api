using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Application.Services;
using FluentValidation;

namespace AnimeService.Application.Validators.AnimeSerialValidators;

public class CreateAnimeDtoValidator : AbstractValidator<CreateAnimeDto>
{
    private readonly ICacheService _cache;
    
    public CreateAnimeDtoValidator(ICacheService cache)
    {
        _cache = cache;
        
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
            .NotEqual(Guid.Empty).WithMessage("Invalid franchise ID.")
            .MustAsync(async (id, cancellationToken) => await AnimeFranchiseIdExists(id, cancellationToken))
            .WithMessage("Anime franchise ID does not exist.");

        RuleFor(x => x.StudioId)
            .NotEmpty().WithMessage("Studio ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid studio ID.")
            .MustAsync(async (id, cancellationToken) => await StudioIdExists(id, cancellationToken))
            .WithMessage("Studio ID does not exist.");
    }

    private async Task<bool> AnimeFranchiseIdExists(Guid animeFranchiseId, CancellationToken cancellationToken)
    {
        var cachedAnimeFranchiseIds = await _cache.GetDataAsync<HashSet<Guid>>("anime-franchise-ids");
        return cachedAnimeFranchiseIds?.Contains(animeFranchiseId) ?? false;
    }
    
    private async Task<bool> StudioIdExists(Guid studioId, CancellationToken cancellationToken)
    {
        var cachedStudioIds = await _cache.GetDataAsync<HashSet<Guid>>("studio-ids");
        return cachedStudioIds?.Contains(studioId) ?? false;
    }
}
