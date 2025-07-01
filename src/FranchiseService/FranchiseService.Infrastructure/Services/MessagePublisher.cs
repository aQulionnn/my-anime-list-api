using FranchiseService.Application.Services;
using MassTransit;
using MessageBroker.Contracts;

namespace FranchiseService.Infrastructure.Services;

public class MessagePublisher(IPublishEndpoint publishEndpoint, IMessageDataRepository repository) : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IMessageDataRepository _repository = repository;
    
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
    {
        if (message is PublishFranchiseCreated input)
        {
            var data = await _repository.PutString(input.Description, cancellationToken);
            await  _publishEndpoint.Publish<FranchiseCreated>(new
            {
                FranchiseId = input.FranchiseId,
                Description = data,
            }, cancellationToken);
        }
    }
}

public record PublishFranchiseCreated(Guid FranchiseId, string Description);