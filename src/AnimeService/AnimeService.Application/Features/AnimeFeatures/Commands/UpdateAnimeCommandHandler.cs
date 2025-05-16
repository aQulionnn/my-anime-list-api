using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeService.Application.Features.AnimeFeatures.Commands;

internal sealed class UpdateAnimeCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeDto> validator) 
    : IRequestHandler<UpdateAnimeCommand, Anime>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeDto> _validator = validator;
    
    public async Task<Anime> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerial = _mapper.Map<Anime>(request.UpdateAnimeDto);
            var result = await _unitOfWork.AnimeRepository.UpdateAsync(request.Id, animeSerial);
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

public record UpdateAnimeCommand(Guid Id, UpdateAnimeDto UpdateAnimeDto) 
    : IRequest<Anime> { }
