using AnimeService.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeService.Domain.Entities;
using AutoMapper;

namespace AnimeService.Application.Mappers;

public class ReWatchedAnimeSerialProfile : Profile
{
    public ReWatchedAnimeSerialProfile()
    {
        CreateMap<CreateReWatchedAnimeSerialDto, ReWatchedAnimeSerial>();
        CreateMap<ReWatchedAnimeSerial, ReWatchedAnimeSerialResponseDto>();
        CreateMap<UpdateReWatchedAnimeSerialDto, ReWatchedAnimeSerial>();
    }
}