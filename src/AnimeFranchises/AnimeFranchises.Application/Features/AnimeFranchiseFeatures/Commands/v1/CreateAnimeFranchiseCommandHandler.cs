using AnimeFranchises.Application.BackgroundTasks.Jobs;
using AnimeFranchises.Application.BackgroundTasks.Requests;
using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v1;

public class CreateAnimeFranchiseCommandHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IValidator<CreateAnimeFranchiseDto> validator, 
        ICacheAnimeFranchiseIdsJob cacheAnimeFranchiseIdsJob,
        IRemoveAnimeFranchiseIdCacheJob removeAnimeFranchiseIdCacheJob
    ) 
    : IRequestHandler<CreateAnimeFranchiseCommand, AnimeFranchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeFranchiseDto> _validator = validator;
    private readonly ICacheAnimeFranchiseIdsJob _cacheAnimeFranchiseIdsJob = cacheAnimeFranchiseIdsJob;
    private readonly IRemoveAnimeFranchiseIdCacheJob _removeAnimeFranchiseIdCacheJob = removeAnimeFranchiseIdCacheJob;
    
    public async Task<AnimeFranchise> Handle(CreateAnimeFranchiseCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
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
            
            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            if (result is not null)
            {
                var removeRequest = new RemoveAnimeFranchiseIdRequest(result.Id);
                await _removeAnimeFranchiseIdCacheJob.PublishAsync(removeRequest, cancellationToken);
            }
            
            throw new Exception(ex.Message);
        }
    }
}

public record CreateAnimeFranchiseCommand(CreateAnimeFranchiseDto CreateAnimeFranchiseDto) 
    : IRequest<AnimeFranchise> { }