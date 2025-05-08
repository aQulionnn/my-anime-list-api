using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using MediatR;

namespace AnimeService.Application.Features.ReWatchedAnimeSerialFeatures.Commands;

public class DeleteReWatchedAnimeSerialCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteReWatchedAnimeSerialCommand, ReWatchedAnimeSerial>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<ReWatchedAnimeSerial> Handle(DeleteReWatchedAnimeSerialCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.ReWatchedAnimeSerialRepository.DeleteAsync(request.Id);
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

public record DeleteReWatchedAnimeSerialCommand(Guid Id) 
    : IRequest<ReWatchedAnimeSerial> { }
