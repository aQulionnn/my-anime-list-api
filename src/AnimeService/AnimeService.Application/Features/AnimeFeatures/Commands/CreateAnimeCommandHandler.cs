using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeService.Application.Features.AnimeFeatures.Commands;

internal sealed class CreateAnimeCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAnimeDto> validator) 
    : IRequestHandler<CreateAnimeCommand, Anime> // было CreateAnimeSerialCommand
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeDto> _validator = validator;
    
    public async Task<Anime> Handle(CreateAnimeCommand request, CancellationToken cancellationToken) // было CreateAnimeSerialCommand
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerial = _mapper.Map<Anime>(request.CreateAnimeDto);
            var result = await _unitOfWork.AnimeRepository.CreateAsync(animeSerial);
            await _unitOfWork.CommitAsync();
            
            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record CreateAnimeCommand(CreateAnimeDto CreateAnimeDto) 
    : IRequest<Anime> { }