using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialFeatures.Commands;

public class DeleteAnimeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeSerialCommand, Anime>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Anime> Handle(DeleteAnimeSerialCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeRepository.DeleteAsync(request.Id);
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

public record DeleteAnimeSerialCommand(Guid Id) 
    : IRequest<Anime> { }
