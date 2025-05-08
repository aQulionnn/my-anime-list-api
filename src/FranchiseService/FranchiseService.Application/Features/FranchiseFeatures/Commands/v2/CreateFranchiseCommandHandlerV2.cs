using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.BackgroundTasks.Jobs;
using FranchiseService.Application.BackgroundTasks.Requests;
using FranchiseService.Application.Dtos.FranchiseDtos;
using FranchiseService.Application.Services;
using MassTransit;
using MediatR;
using MessageBroker.Contracts;

namespace FranchiseService.Application.Features.AnimeFranchiseFeatures.Commands.v2;

public class CreateFranchiseCommandHandlerV2
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IValidator<CreateFranchiseDto> validator, 
        ICacheFranchiseIdsJob cacheFranchiseIdsJob,
        IRemoveFranchiseIdCacheJob removeFranchiseIdCacheJob,
        IPublishEndpoint publishEndpoint,
        ICacheService cache
    )
    : IRequestHandler<CreateAnimeFranchiseCommandV2, Result<Franchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateFranchiseDto> _validator = validator;
    private readonly ICacheFranchiseIdsJob _cacheFranchiseIdsJob = cacheFranchiseIdsJob;
    private readonly IRemoveFranchiseIdCacheJob _removeFranchiseIdCacheJob = removeFranchiseIdCacheJob;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly ICacheService _cache = cache;
    
    public async Task<Result<Franchise>> Handle(CreateAnimeFranchiseCommandV2 request, CancellationToken cancellationToken)
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

            var cacheRequest = new CacheFranchiseIdsRequest(result.Id);
            await _cacheFranchiseIdsJob.PublishAsync(cacheRequest, cancellationToken);

            await _publishEndpoint.Publish(new FranchiseCreated(result.Id));
            await _cache.SetDataAsync($"franchises:{result.Id}", true, TimeSpan.FromHours(1));
            
            return Result<Franchise>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            if (result is not null)
            {
                var removeRequest = new RemoveFranchiseIdRequest(result.Id);
                await _removeFranchiseIdCacheJob.PublishAsync(removeRequest, cancellationToken);
            }

            return Result<Franchise>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record CreateAnimeFranchiseCommandV2(CreateFranchiseDto CreateFranchiseDto) 
    : IRequest<Result<Franchise>> { }