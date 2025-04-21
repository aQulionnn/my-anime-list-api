using AnimeFranchises.Domain.Enums;

namespace AnimeFranchises.Application.Dtos.FranchiseTranslationDtos;

public class CreateFranchiseTranslationDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }
    
    public Guid AnimeFranchiseId { get; set; }    
}