using FranchiseService.Domain.Enums;

namespace FranchiseService.Application.Dtos.FranchiseTranslationDtos;

public class UpdateFranchiseTranslationDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }
}