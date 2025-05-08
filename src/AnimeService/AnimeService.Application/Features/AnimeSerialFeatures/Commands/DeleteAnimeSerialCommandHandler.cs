using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using MediatR;

namespace AnimeService.Application.Features.AnimeSerialFeatures.Commands;

public class DeleteAnimeSerialCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeSerialCommand, AnimeSerial>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<AnimeSerial> Handle(DeleteAnimeSerialCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeSerialRepository.DeleteAsync(request.Id);
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
    : IRequest<AnimeSerial> { }
