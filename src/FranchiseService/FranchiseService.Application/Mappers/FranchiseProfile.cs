using FranchiseService.Domain.Entities;
using AutoMapper;
using FranchiseService.Application.Dtos.FranchiseDtos;

namespace FranchiseService.Application.Mappers;

public class FranchiseProfile : Profile
{
    public FranchiseProfile()
    {
        CreateMap<CreateFranchiseDto, Franchise>();
        CreateMap<Franchise, FranchiseResponseDto>();
        CreateMap<UpdateFranchiseDto, Franchise>();
    }
}