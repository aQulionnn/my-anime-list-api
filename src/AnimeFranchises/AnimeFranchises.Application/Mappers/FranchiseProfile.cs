using AnimeFranchises.Application.Dtos.FranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AutoMapper;

namespace AnimeFranchises.Application.Mappers;

public class FranchiseProfile : Profile
{
    public FranchiseProfile()
    {
        CreateMap<CreateFranchiseDto, Franchise>();
        CreateMap<Franchise, FranchiseResponseDto>();
        CreateMap<UpdateFranchiseDto, Franchise>();
    }
}