using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands;

public class DeleteAnimeFranchiseInfoCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeFranchiseInfoCommand, AnimeFranchiseInfo>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<AnimeFranchiseInfo> Handle(DeleteAnimeFranchiseInfoCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.DeleteAsync(request.Id);
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

public record DeleteAnimeFranchiseInfoCommand(Guid Id)
    : IRequest<AnimeFranchiseInfo>;