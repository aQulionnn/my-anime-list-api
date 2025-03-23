using AnimeFranchises.Domain.Enums;

namespace AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;

public class AnimeFranchiseInfoResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }
    
    public Guid AnimeFranchiseId { get; set; }    
}