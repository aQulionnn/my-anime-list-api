using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands;

public class DeleteAnimeFranchiseCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeFranchiseCommand, AnimeFranchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<AnimeFranchise> Handle(DeleteAnimeFranchiseCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeFranchiseRepository.DeleteAsync(request.Id);
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

public record DeleteAnimeFranchiseCommand(Guid Id)
    : IRequest<AnimeFranchise> { }