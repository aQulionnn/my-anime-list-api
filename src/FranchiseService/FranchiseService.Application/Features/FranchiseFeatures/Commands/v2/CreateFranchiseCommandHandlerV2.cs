using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseDtos;
using FranchiseService.Application.Services;
using MassTransit;
using MediatR;
using MessageBroker.Contracts;
using SharedKernel.Shared;

namespace FranchiseService.Application.Features.FranchiseFeatures.Commands.v2;

internal sealed class CreateFranchiseCommandHandlerV2
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IValidator<CreateFranchiseDto> validator, 
        IMessagePublisher messagePublisher,
        ICacheService cache
    )
    : IRequestHandler<CreateFranchiseCommandV2, Result<Franchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateFranchiseDto> _validator = validator;
    private readonly IMessagePublisher _messagePublisher = messagePublisher;
    private readonly ICacheService _cache = cache;
    
    public async Task<Result<Franchise>> Handle(CreateFranchiseCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<Franchise>.Failure(Error.ValidationFailed(validation.Errors));
        }
        
        Franchise? result = null;
        
        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<Franchise>(request.CreateFranchiseDto);
            result = await _unitOfWork.FranchiseRepository.CreateAsync(animeFranchise);
            await _unitOfWork.CommitAsync();

            await _messagePublisher.PublishAsync(new PublishFranchiseCreated(result.Id, "created"), cancellationToken);
            await _cache.SetDataAsync($"franchises:{result.Id}", true, TimeSpan.FromHours(1));
            
            return Result<Franchise>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Franchise>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record CreateFranchiseCommandV2(CreateFranchiseDto CreateFranchiseDto) 
    : IRequest<Result<Franchise>> { }
    
public record PublishFranchiseCreated(Guid FranchiseId, string Description);