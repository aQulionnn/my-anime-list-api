using MassTransit;

namespace MessageBroker.Contracts;

public interface FranchiseCreated
{
    Guid FranchiseId { get; }
    MessageData<string> Description { get; }
}