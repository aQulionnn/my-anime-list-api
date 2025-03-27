using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Entities;
using AutoMapper;

namespace AnimeFranchises.Application.Mappers;

public class AnimeFranchiseInfoProfile : Profile
{
    public AnimeFranchiseInfoProfile()
    {
        CreateMap<CreateAnimeFranchiseInfoDto, AnimeFranchiseInfo>();
        CreateMap<AnimeFranchiseInfo, AnimeFranchiseInfoResponseDto>()
            .ForMember(dest => dest.Language, src => src.MapFrom(x => x.Language.ToString()));
        CreateMap<UpdateAnimeFranchiseInfoDto, AnimeFranchiseInfo>();
    }
}