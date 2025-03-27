using AnimeSeries.Application.Dtos.AnimeSerialInfoDtos;
using AnimeSeries.Domain.Entities;
using AutoMapper;

namespace AnimeSeries.Application.Mappers;

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