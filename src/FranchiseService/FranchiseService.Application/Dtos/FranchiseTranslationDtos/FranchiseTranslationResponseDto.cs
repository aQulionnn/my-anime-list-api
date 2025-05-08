using FranchiseService.Domain.Enums;

namespace FranchiseService.Application.Dtos.FranchiseTranslationDtos;

public class FranchiseTranslationResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    
    public Guid AnimeFranchiseId { get; set; }    
}