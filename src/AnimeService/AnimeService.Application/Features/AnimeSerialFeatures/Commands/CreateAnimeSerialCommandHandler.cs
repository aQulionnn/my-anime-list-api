using AnimeService.Application.Dtos.AnimeSerialDtos;
using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialFeatures.Commands;

public class CreateAnimeSerialCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAnimeSerialDto> validator) 
    : IRequestHandler<CreateAnimeSerialCommand, AnimeSerial>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeSerialDto> _validator = validator;
    
    public async Task<AnimeSerial> Handle(CreateAnimeSerialCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeSerialDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerial = _mapper.Map<AnimeSerial>(request.CreateAnimeSerialDto);
            var result = await _unitOfWork.AnimeSerialRepository.CreateAsync(animeSerial);
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

public record CreateAnimeSerialCommand(CreateAnimeSerialDto CreateAnimeSerialDto) 
    : IRequest<AnimeSerial> { }
