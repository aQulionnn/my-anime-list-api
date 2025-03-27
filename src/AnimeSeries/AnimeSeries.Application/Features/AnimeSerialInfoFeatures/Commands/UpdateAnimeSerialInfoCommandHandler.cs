using AnimeSeries.Application.Dtos.AnimeSerialInfoDtos;
using AnimeSeries.Domain.Entities;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeSeries.Application.Features.AnimeSerialInfoFeatures.Commands;

public class UpdateAnimeSerialInfoCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeSerialInfoDto> validator) 
    : IRequestHandler<UpdateAnimeSerialInfoCommand, AnimeSerialInfo>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeSerialInfoDto> _validator = validator;
    
    public async Task<AnimeSerialInfo> Handle(UpdateAnimeSerialInfoCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeSerialInfoDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerialInfo = _mapper.Map<AnimeSerialInfo>(request.UpdateAnimeSerialInfoDto);
            var result = await _unitOfWork.AnimeSerialInfoRepository.UpdateAsync(request.Id, animeSerialInfo);
            await _unitOfWork.CommitAsync();
            
            return animeSerialInfo;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record UpdateAnimeSerialInfoCommand(Guid Id, UpdateAnimeSerialInfoDto UpdateAnimeSerialInfoDto) 
    : IRequest<AnimeSerialInfo> { }
