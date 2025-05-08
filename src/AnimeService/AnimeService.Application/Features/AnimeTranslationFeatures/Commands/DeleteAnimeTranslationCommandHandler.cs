using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialInfoFeatures.Commands;

public class DeleteAnimeTranslationCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeSerialInfoCommand, AnimeTranslation>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<AnimeTranslation> Handle(DeleteAnimeSerialInfoCommand request, CancellationToken cancellationToken)
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

public record DeleteAnimeSerialInfoCommand(Guid Id) 
    : IRequest<AnimeTranslation> { }
