using AnimeSeries.Application.Dtos.AnimeSerialDtos;
using AnimeSeries.Domain.Entities;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeSeries.Application.Features.AnimeSerialFeatures.Commands;

public class UpdateAnimeSerialCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeSerialDto> validator) 
    : IRequestHandler<UpdateAnimeSerialCommand, AnimeSerial>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeSerialDto> _validator = validator;
    
    public async Task<AnimeSerial> Handle(UpdateAnimeSerialCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeSerialDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerial = _mapper.Map<AnimeSerial>(request.UpdateAnimeSerialDto);
            var result = await _unitOfWork.AnimeSerialRepository.UpdateAsync(request.Id, animeSerial);
            await _unitOfWork.CommitAsync();
            
            return animeSerial;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record UpdateAnimeSerialCommand(Guid Id, UpdateAnimeSerialDto UpdateAnimeSerialDto) 
    : IRequest<AnimeSerial> { }
