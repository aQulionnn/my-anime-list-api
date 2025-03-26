using AnimeSeries.Domain.Entities;
using AnimeSeries.Domain.Interfaces;
using MediatR;

namespace AnimeSeries.Application.Features.AnimeSerialInfoFeatures.Commands;

public class DeleteAnimeSerialInfoCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeSerialInfoCommand, AnimeSerialInfo>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<AnimeSerialInfo> Handle(DeleteAnimeSerialInfoCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeSerialInfoRepository.DeleteAsync(request.Id);
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
    : IRequest<AnimeSerialInfo> { }
