using AnimeSeries.Application.Dtos.AnimeSerialDtos;
using AnimeSeries.Domain.Entities;
using AutoMapper;

namespace AnimeSeries.Application.Mappers;

public class AnimeSerialProfile : Profile
{
    public AnimeSerialProfile()
    {
        CreateMap<CreateAnimeSerialDto, AnimeSerial>();
        CreateMap<AnimeSerial, AnimeSerialResponseDto>();
        CreateMap<UpdateAnimeSerialDto, AnimeSerial>();
    }
}