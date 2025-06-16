using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MessageBroker;

public class MessagingDbContext(DbContextOptions<MessagingDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
    }
}