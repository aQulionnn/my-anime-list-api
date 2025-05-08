using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Domain.Entities;
using AutoMapper;

namespace AnimeService.Application.Mappers;

public class AnimeSerialInfoProfile : Profile
{
    public AnimeSerialInfoProfile()
    {
        CreateMap<CreateAnimeSerialInfoDto, AnimeSerialInfo>();
        CreateMap<AnimeSerialInfo, AnimeSerialInfoResponseDto>()
            .ForMember(dest => dest.Language, src => src.MapFrom(x => x.Language.ToString()));
        CreateMap<UpdateAnimeSerialInfoDto, AnimeSerialInfo>();
    }
}