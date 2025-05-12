using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using MediatR;
using SharedKernel.Shared;

namespace FranchiseService.Application.Features.FranchiseTranslationFeatures.Commands.v2;

public class DeleteFranchiseTranslationCommandHandlerV2
    (IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteFranchiseTranslationCommandV2, Result<FranchiseTranslation>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Result<FranchiseTranslation>> Handle(DeleteFranchiseTranslationCommandV2 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.FranchiseTranslationRepository.DeleteAsync(request.Id);
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

public record DeleteFranchiseTranslationCommandV2(Guid Id)
    : IRequest<Result<FranchiseTranslation>>;
