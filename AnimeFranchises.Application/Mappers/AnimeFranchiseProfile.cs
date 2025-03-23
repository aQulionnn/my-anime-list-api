using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AutoMapper;

namespace AnimeFranchises.Application.Mappers;

public class AnimeFranchiseProfile : Profile
{
    public AnimeFranchiseProfile()
    {
        CreateMap<CreateAnimeFranchiseDto, AnimeFranchise>();
        CreateMap<AnimeFranchise, AnimeFranchiseResponseDto>();
        CreateMap<UpdateAnimeFranchiseDto, AnimeFranchise>();
    }
}