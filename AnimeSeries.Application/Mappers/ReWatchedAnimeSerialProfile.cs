using AnimeSeries.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeSeries.Domain.Entities;
using AutoMapper;

namespace AnimeSeries.Application.Mappers;

public class ReWatchedAnimeSerialProfile : Profile
{
    public ReWatchedAnimeSerialProfile()
    {
        CreateMap<CreateReWatchedAnimeSerialDto, ReWatchedAnimeSerial>();
        CreateMap<ReWatchedAnimeSerial, ReWatchedAnimeSerialResponseDto>();
        CreateMap<UpdateReWatchedAnimeSerialDto, ReWatchedAnimeSerial>();
    }
}