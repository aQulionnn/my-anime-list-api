using AnimeFranchises.Application.Dtos.FranchiseTranslationDtos;
using AnimeFranchises.Domain.Entities;
using AutoMapper;

namespace AnimeFranchises.Application.Mappers;

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