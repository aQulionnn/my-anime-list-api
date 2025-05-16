using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseTranslationFeatures.Commands.v1;

internal sealed class DeleteFranchiseTranslationCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteFranchiseTranslationCommand, FranchiseTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<FranchiseTranslation> Handle(DeleteFranchiseTranslationCommand request, CancellationToken cancellationToken)
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

public record DeleteFranchiseTranslationCommand(Guid Id)
    : IRequest<FranchiseTranslation>;