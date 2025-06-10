using FranchiseService.Application.Services;
using FranchiseService.Infrastructure.Services;
using Polly;

namespace FranchiseService.Infrastructure.Decorators;

public class ResilientRedisCacheService(RedisCacheService redis) 
    : ICacheService
{
    private readonly RedisCacheService _redis = redis;
    
    public async Task<T?> GetDataAsync<T>(string key)
    {
        var fallback = Policy<T?>
            .Handle<Exception>()
            .FallbackAsync(default(T?));
        
        return await fallback.ExecuteAsync(async () => await _redis.GetDataAsync<T>(key));     
    }

    public async Task SetDataAsync<T>(string key, T data, TimeSpan ttl)
    {
        var fallback = Policy
            .Handle<Exception>()
            .FallbackAsync(fallbackAction: async (context, cancellationToken) => await Task.CompletedTask,
                onFallbackAsync: async (exception, context) => await Task.CompletedTask);
        
        await fallback.ExecuteAsync(async () => 
            await _redis.SetDataAsync(key, data, ttl));
    }

    public async Task RemoveDataAsync<T>(string key)
    {
        var fallback = Policy
            .Handle<Exception>()
            .FallbackAsync(fallbackAction: async (context, cancellationToken) => await Task.CompletedTask,
                onFallbackAsync: async (exception, context) => await Task.CompletedTask);
        
        await fallback.ExecuteAsync(async () => 
            await _redis.RemoveDataAsync<T>(key));
    }
}