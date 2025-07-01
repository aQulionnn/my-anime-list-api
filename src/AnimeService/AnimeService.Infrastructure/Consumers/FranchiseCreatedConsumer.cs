using AnimeService.Domain.Entities;
using AnimeService.Infrastructure.Data;
using MassTransit;
using MessageBroker.Contracts;

namespace AnimeService.Infrastructure.Consumers;

public class FranchiseCreatedConsumer(AnimeServiceDbContext dbContext) : IConsumer<FranchiseCreated>
{
    private readonly AnimeServiceDbContext _dbContext = dbContext;
    
    public async Task Consume(ConsumeContext<FranchiseCreated> context)
    {
        var description = await context.Message.Description.Value;
        
        var franchiseRef = new FranchiseReference
        {
            FranchiseId = context.Message.FranchiseId,
        };
        await _dbContext.FranchiseReferences.AddAsync(franchiseRef);
        await _dbContext.SaveChangesAsync();
    }
}