using AnimeSeries.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeSeries.Application.Features.ReWatchedAnimeSerialFeatures.Queries;

public class GetReWatchedAnimeSerialByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetReWatchedAnimeSerialByIdQuery, ReWatchedAnimeSerialResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ReWatchedAnimeSerialResponseDto> Handle(GetReWatchedAnimeSerialByIdQuery request, CancellationToken cancellationToken)
    {
        var reWatchedAnimeSerial = await _unitOfWork.ReWatchedAnimeSerialRepository.GetByIdAsync(request.Id);
        return _mapper.Map<ReWatchedAnimeSerialResponseDto>(reWatchedAnimeSerial);
    }
}

public record GetReWatchedAnimeSerialByIdQuery(Guid Id) : IRequest<ReWatchedAnimeSerialResponseDto> { }
