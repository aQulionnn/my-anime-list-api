using AnimeService.Application.Dtos.AnimeSerialDtos;
using AnimeService.Domain.Entities;
using AutoMapper;

namespace AnimeService.Application.Mappers;

public class AnimeSerialProfile : Profile
{
    public AnimeSerialProfile()
    {
        CreateMap<CreateAnimeSerialDto, AnimeSerial>();
        CreateMap<AnimeSerial, AnimeSerialResponseDto>();
        CreateMap<UpdateAnimeSerialDto, AnimeSerial>();
    }
}