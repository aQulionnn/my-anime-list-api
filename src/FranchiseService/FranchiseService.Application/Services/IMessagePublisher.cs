namespace FranchiseService.Application.Services;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message,CancellationToken cancellationToken);
}