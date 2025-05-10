using FranchiseService.Application.Services;
using MassTransit;

namespace FranchiseService.Infrastructure.Services;

public class MessagePublisher(IPublishEndpoint publishEndpoint) : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
    {
        await  _publishEndpoint.Publish(message!, cancellationToken);
    }
}