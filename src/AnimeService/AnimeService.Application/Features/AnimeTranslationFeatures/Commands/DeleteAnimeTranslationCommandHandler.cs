using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using MediatR;

namespace AnimeService.Application.Features.AnimeTranslationFeatures.Commands;

internal sealed class DeleteAnimeTranslationCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeTranslationCommand, AnimeTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<AnimeTranslation> Handle(DeleteAnimeTranslationCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeTranslationRepository.DeleteAsync(request.Id);
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

public record DeleteAnimeTranslationCommand(Guid Id) 
    : IRequest<AnimeTranslation> { }
