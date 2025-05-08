using AnimeService.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeService.Application.Features.ReWatchedAnimeSerialFeatures.Commands;

public class UpdateReWatchedAnimeSerialCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateReWatchedAnimeSerialDto> validator) 
    : IRequestHandler<UpdateReWatchedAnimeSerialCommand, ReWatchedAnimeSerial>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateReWatchedAnimeSerialDto> _validator = validator;
    
    public async Task<ReWatchedAnimeSerial> Handle(UpdateReWatchedAnimeSerialCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateReWatchedAnimeSerialDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var reWatchedAnimeSerial = _mapper.Map<ReWatchedAnimeSerial>(request.UpdateReWatchedAnimeSerialDto);
            var result = await _unitOfWork.ReWatchedAnimeSerialRepository.UpdateAsync(request.Id, reWatchedAnimeSerial);
            await _unitOfWork.CommitAsync();
            
            return reWatchedAnimeSerial;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record UpdateReWatchedAnimeSerialCommand(Guid Id, UpdateReWatchedAnimeSerialDto UpdateReWatchedAnimeSerialDto) 
    : IRequest<ReWatchedAnimeSerial> { }
