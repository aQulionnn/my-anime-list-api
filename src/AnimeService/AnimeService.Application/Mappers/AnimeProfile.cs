using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Domain.Entities;
using AutoMapper;

namespace AnimeService.Application.Mappers;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<CreateAnimeDto, Anime>();
        CreateMap<Anime, AnimeResponseDto>();
        CreateMap<UpdateAnimeDto, Anime>();
    }
}