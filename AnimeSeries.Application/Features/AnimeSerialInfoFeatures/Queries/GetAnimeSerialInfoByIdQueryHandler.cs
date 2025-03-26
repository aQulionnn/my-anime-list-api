using AnimeSeries.Application.Dtos.AnimeSerialInfoDtos;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeSeries.Application.Features.AnimeSerialInfoFeatures.Queries;

public class GetAnimeSerialInfoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeSerialInfoByIdQuery, AnimeSerialInfoResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<AnimeSerialInfoResponseDto> Handle(GetAnimeSerialInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var animeSerialInfo = await _unitOfWork.AnimeSerialInfoRepository.GetByIdAsync(request.Id);
        return _mapper.Map<AnimeSerialInfoResponseDto>(animeSerialInfo);
    }
}

public record GetAnimeSerialInfoByIdQuery(Guid Id) : IRequest<AnimeSerialInfoResponseDto> { }
