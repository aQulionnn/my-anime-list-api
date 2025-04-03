using AnimeFranchises.Application.BackgroundTasks.Jobs;
using AnimeFranchises.Application.BackgroundTasks.Requests;
using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v2;

public class CreateAnimeFranchiseCommandHandlerV2
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IValidator<CreateAnimeFranchiseDto> validator, 
        ICacheAnimeFranchiseIdsJob cacheAnimeFranchiseIdsJob,
        IRemoveAnimeFranchiseIdCacheJob removeAnimeFranchiseIdCacheJob    
    )
    : IRequestHandler<CreateAnimeFranchiseCommandV2, Result<AnimeFranchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeFranchiseDto> _validator = validator;
    private readonly ICacheAnimeFranchiseIdsJob _cacheAnimeFranchiseIdsJob = cacheAnimeFranchiseIdsJob;
    private readonly IRemoveAnimeFranchiseIdCacheJob _removeAnimeFranchiseIdCacheJob = removeAnimeFranchiseIdCacheJob;
    
    public async Task<Result<AnimeFranchise>> Handle(CreateAnimeFranchiseCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<AnimeFranchise>.Failure(400, new Error("Validation failed", validation.Errors));
        }
        
        AnimeFranchise? result = null;
        
        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<AnimeFranchise>(request.CreateAnimeFranchiseDto);
            result = await _unitOfWork.AnimeFranchiseRepository.CreateAsync(animeFranchise);
            await _unitOfWork.CommitAsync();

            var cacheRequest = new CacheAnimeFranchiseIdsRequest(result.Id);
            await _cacheAnimeFranchiseIdsJob.PublishAsync(cacheRequest, cancellationToken);

            return Result<AnimeFranchise>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            if (result is not null)
            {
                var removeRequest = new RemoveAnimeFranchiseIdRequest(result.Id);
                await _removeAnimeFranchiseIdCacheJob.PublishAsync(removeRequest, cancellationToken);
            }

            return Result<AnimeFranchise>.Failure(500, new Error(ex.Message, ex));
        }
    }
}

public record CreateAnimeFranchiseCommandV2(CreateAnimeFranchiseDto CreateAnimeFranchiseDto) 
    : IRequest<Result<AnimeFranchise>> { }