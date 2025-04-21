using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v2;

public class DeleteFranchiseCommandHandlerV2
    (IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeFranchiseCommandV2, Result<Franchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Franchise>> Handle(DeleteAnimeFranchiseCommandV2 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeFranchiseRepository.DeleteAsync(request.Id);
            if (result == null)
            {
                return Result<Franchise>.Failure(Error.NotFound());
            }

            await _unitOfWork.CommitAsync();
            return Result<Franchise>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Franchise>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record DeleteAnimeFranchiseCommandV2(Guid Id)
    : IRequest<Result<Franchise>> { }
