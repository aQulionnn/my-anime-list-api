using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialInfoFeatures.Commands;

public class CreateAnimeSerialInfoCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAnimeSerialInfoDto> validator) 
    : IRequestHandler<CreateAnimeSerialInfoCommand, AnimeSerialInfo>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeSerialInfoDto> _validator = validator;
    
    public async Task<AnimeSerialInfo> Handle(CreateAnimeSerialInfoCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeSerialInfoDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeSerialInfo = _mapper.Map<AnimeSerialInfo>(request.CreateAnimeSerialInfoDto);
            var result = await _unitOfWork.AnimeSerialInfoRepository.CreateAsync(animeSerialInfo);
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

public record CreateAnimeSerialInfoCommand(CreateAnimeSerialInfoDto CreateAnimeSerialInfoDto) 
    : IRequest<AnimeSerialInfo> { }
