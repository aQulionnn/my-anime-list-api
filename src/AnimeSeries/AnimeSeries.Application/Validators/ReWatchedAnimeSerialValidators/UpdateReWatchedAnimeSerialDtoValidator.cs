using AnimeSeries.Application.Dtos.ReWatchedAnimeSerialDtos;
using FluentValidation;

namespace AnimeSeries.Application.Validators.ReWatchedAnimeSerialValidators;

public class UpdateReWatchedAnimeSerialDtoValidator : AbstractValidator<UpdateReWatchedAnimeSerialDto>
{
    public UpdateReWatchedAnimeSerialDtoValidator()
    {
        RuleFor(x => x.ReWatchedEpisodes)
            .GreaterThanOrEqualTo(0).WithMessage("Rewatched episodes must be zero or more.");

        RuleFor(x => x.ViewingOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Viewing order must be zero or more.");
    }
}