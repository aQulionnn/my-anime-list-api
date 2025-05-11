using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseFeatures.Commands.v1;

public class DeleteFranchiseCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteFranchiseCommand, Franchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Franchise> Handle(DeleteFranchiseCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.FranchiseRepository.DeleteAsync(request.Id);
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

public record DeleteFranchiseCommand(Guid Id)
    : IRequest<Franchise> { }