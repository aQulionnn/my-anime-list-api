using AnimeFranchises.Domain.Enums;

namespace AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;

public class UpdateAnimeFranchiseInfoDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }
    
    public Guid AnimeFranchiseId { get; set; }    
}