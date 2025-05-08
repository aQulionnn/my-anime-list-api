using FranchiseService.Domain.Enums;

namespace FranchiseService.Application.Dtos.FranchiseTranslationDtos;

public class CreateFranchiseTranslationDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }
    
    public Guid AnimeFranchiseId { get; set; }    
}