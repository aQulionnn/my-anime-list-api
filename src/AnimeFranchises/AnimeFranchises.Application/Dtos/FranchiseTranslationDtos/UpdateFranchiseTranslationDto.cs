using AnimeFranchises.Domain.Enums;

namespace AnimeFranchises.Application.Dtos.FranchiseTranslationDtos;

public class UpdateFranchiseTranslationDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }
}