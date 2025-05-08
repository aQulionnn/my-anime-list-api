using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Domain.Entities;
using AutoMapper;

namespace AnimeService.Application.Mappers;

public class AnimeTranslationProfile : Profile
{
    public AnimeTranslationProfile()
    {
        CreateMap<CreateAnimeTranslationDto, AnimeTranslation>();
        CreateMap<AnimeTranslation, AnimeTranslationResponseDto>()
            .ForMember(dest => dest.Language, src => src.MapFrom(x => x.Language.ToString()));
        CreateMap<UpdateAnimeTranslationDto, AnimeTranslation>();
    }
}