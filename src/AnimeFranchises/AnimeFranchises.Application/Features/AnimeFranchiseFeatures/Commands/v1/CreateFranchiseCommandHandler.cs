using AnimeFranchises.Application.BackgroundTasks.Jobs;
using AnimeFranchises.Application.BackgroundTasks.Requests;
using AnimeFranchises.Application.Dtos.FranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v1;

public class CreateFranchiseCommandHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IValidator<CreateFranchiseDto> validator, 
        ICacheFranchiseIdsJob cacheFranchiseIdsJob,
        IRemoveFranchiseIdCacheJob removeFranchiseIdCacheJob
    ) 
    : IRequestHandler<CreateAnimeFranchiseCommand, Franchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateFranchiseDto> _validator = validator;
    private readonly ICacheFranchiseIdsJob _cacheFranchiseIdsJob = cacheFranchiseIdsJob;
    private readonly IRemoveFranchiseIdCacheJob _removeFranchiseIdCacheJob = removeFranchiseIdCacheJob;
    
    public async Task<Franchise> Handle(CreateAnimeFranchiseCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        Franchise? result = null;
        
        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<Franchise>(request.CreateFranchiseDto);
            result = await _unitOfWork.AnimeFranchiseRepository.CreateAsync(animeFranchise);
            await _unitOfWork.CommitAsync();
            
            var cacheRequest = new CacheFranchiseIdsRequest(result.Id);
            await _cacheFranchiseIdsJob.PublishAsync(cacheRequest, cancellationToken);
            
            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            if (result is not null)
            {
                var removeRequest = new RemoveFranchiseIdRequest(result.Id);
                await _removeFranchiseIdCacheJob.PublishAsync(removeRequest, cancellationToken);
            }
            
            throw new Exception(ex.Message);
        }
    }
}

public record CreateAnimeFranchiseCommand(CreateFranchiseDto CreateFranchiseDto) 
    : IRequest<Franchise> { }