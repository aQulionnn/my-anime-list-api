using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using MediatR;

namespace FranchiseService.Application.Features.AnimeFranchiseInfoFeatures.Commands.v1;

public class DeleteFranchiseTranslationCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeFranchiseInfoCommand, FranchiseTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<FranchiseTranslation> Handle(DeleteAnimeFranchiseInfoCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.FranchiseTranslationRepository.DeleteAsync(request.Id);
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
    : IRequest<FranchiseTranslation>;