using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using MediatR;
using SharedKernel.Shared;

namespace FranchiseService.Application.Features.FranchiseFeatures.Commands.v2;

public class DeleteFranchiseCommandHandlerV2
    (IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteFranchiseCommandV2, Result<Franchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Franchise>> Handle(DeleteFranchiseCommandV2 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.FranchiseRepository.DeleteAsync(request.Id);
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

public record DeleteFranchiseCommandV2(Guid Id)
    : IRequest<Result<Franchise>> { }
