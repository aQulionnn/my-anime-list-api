using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeService.Application.Features.AnimeTranslationFeatures.Commands;

internal sealed class CreateAnimeTranslationCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAnimeTranslationDto> validator) 
    : IRequestHandler<CreateAnimeTranslationCommand, AnimeTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeTranslationDto> _validator = validator;
    
    public async Task<AnimeTranslation> Handle(CreateAnimeTranslationCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeTranslationDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerialInfo = _mapper.Map<AnimeTranslation>(request.CreateAnimeTranslationDto);
            var result = await _unitOfWork.AnimeTranslationRepository.CreateAsync(animeSerialInfo);
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

public record CreateAnimeTranslationCommand(CreateAnimeTranslationDto CreateAnimeTranslationDto) 
    : IRequest<AnimeTranslation> { }
