using FranchiseService.Domain.Entities;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;

namespace FranchiseService.Application.Mappers;

public class FranchiseTranslationProfile : Profile
{
    public FranchiseTranslationProfile()
    {
        CreateMap<CreateFranchiseTranslationDto, FranchiseTranslation>();
        CreateMap<FranchiseTranslation, FranchiseTranslationResponseDto>()
            .ForMember(dest => dest.Language, src => src.MapFrom(x => x.Language.ToString()));
        CreateMap<UpdateFranchiseTranslationDto, FranchiseTranslation>();
    }
}