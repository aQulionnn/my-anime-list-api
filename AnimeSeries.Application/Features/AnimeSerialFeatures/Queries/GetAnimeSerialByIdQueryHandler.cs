using AnimeSeries.Application.Dtos.AnimeSerialDtos;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeSeries.Application.Features.AnimeSerialFeatures.Queries;

public class GetAnimeSerialByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAnimeSerialByIdQuery, AnimeSerialResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<AnimeSerialResponseDto> Handle(GetAnimeSerialByIdQuery request, CancellationToken cancellationToken)
    {
        var animeSerial = await _unitOfWork.AnimeSerialRepository.GetByIdAsync(request.Id);
        return _mapper.Map<AnimeSerialResponseDto>(animeSerial);
    }
}

public record GetAnimeSerialByIdQuery(Guid Id) : IRequest<AnimeSerialResponseDto> { }
