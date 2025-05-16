using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeService.Application.Features.AnimeTranslationFeatures.Commands;

internal sealed class UpdateAnimeTranslationCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeTranslationDto> validator) 
    : IRequestHandler<UpdateAnimeTranslationCommand, AnimeTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeTranslationDto> _validator = validator;
    
    public async Task<AnimeTranslation> Handle(UpdateAnimeTranslationCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeTranslationDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerialInfo = _mapper.Map<AnimeTranslation>(request.UpdateAnimeTranslationDto);
            var result = await _unitOfWork.AnimeTranslationRepository.UpdateAsync(request.Id, animeSerialInfo);
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

public record UpdateAnimeTranslationCommand(Guid Id, UpdateAnimeTranslationDto UpdateAnimeTranslationDto) 
    : IRequest<AnimeTranslation> { }
