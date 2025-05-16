using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using MediatR;

namespace AnimeService.Application.Features.AnimeFeatures.Commands;

internal sealed class DeleteAnimeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeCommand, Anime>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Anime> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
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

public record DeleteAnimeCommand(Guid Id) 
    : IRequest<Anime> { }
