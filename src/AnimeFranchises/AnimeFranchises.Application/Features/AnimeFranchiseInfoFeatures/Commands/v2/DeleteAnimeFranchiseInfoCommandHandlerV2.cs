using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands.v2;

public class DeleteAnimeFranchiseInfoCommandHandlerV2
    (IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeFranchiseInfoCommandV2, Result<AnimeFranchiseInfo>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Result<AnimeFranchiseInfo>> Handle(DeleteAnimeFranchiseInfoCommandV2 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();
            
            return Result<AnimeFranchiseInfo>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<AnimeFranchiseInfo>.Failure(500, new Error(ex.Message, null));
        }
    }
}

public record DeleteAnimeFranchiseInfoCommandV2(Guid Id)
    : IRequest<Result<AnimeFranchiseInfo>>;
