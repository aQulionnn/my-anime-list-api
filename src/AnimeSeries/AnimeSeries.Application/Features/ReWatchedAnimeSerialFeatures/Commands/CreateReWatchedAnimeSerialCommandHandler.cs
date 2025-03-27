using AnimeSeries.Application.Dtos.ReWatchedAnimeSerialDtos;
using AnimeSeries.Domain.Entities;
using AnimeSeries.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeSeries.Application.Features.ReWatchedAnimeSerialFeatures.Commands;

public class CreateReWatchedAnimeSerialCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateReWatchedAnimeSerialDto> validator) 
    : IRequestHandler<CreateReWatchedAnimeSerialCommand, ReWatchedAnimeSerial>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateReWatchedAnimeSerialDto> _validator = validator;
    
    public async Task<ReWatchedAnimeSerial> Handle(CreateReWatchedAnimeSerialCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateReWatchedAnimeSerialDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var reWatchedAnimeSerial = _mapper.Map<ReWatchedAnimeSerial>(request.CreateReWatchedAnimeSerialDto);
            var result = await _unitOfWork.ReWatchedAnimeSerialRepository.CreateAsync(reWatchedAnimeSerial);
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

public record CreateReWatchedAnimeSerialCommand(CreateReWatchedAnimeSerialDto CreateReWatchedAnimeSerialDto) 
    : IRequest<ReWatchedAnimeSerial> { }
