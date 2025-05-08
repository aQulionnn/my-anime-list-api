using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using MediatR;

namespace FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Commands.v2;

public class DeleteFranchiseTranslationCommandHandlerV2
    (IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeFranchiseInfoCommandV2, Result<FranchiseTranslation>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Result<FranchiseTranslation>> Handle(DeleteAnimeFranchiseInfoCommandV2 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();
            
            return Result<FranchiseTranslation>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<FranchiseTranslation>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record DeleteAnimeFranchiseInfoCommandV2(Guid Id)
    : IRequest<Result<FranchiseTranslation>>;
