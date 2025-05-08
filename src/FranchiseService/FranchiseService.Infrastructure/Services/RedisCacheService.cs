using System.Text.Json;
using FranchiseService.Application.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace FranchiseService.Infrastructure.Services;

public class RedisCacheService(IDistributedCache cache) : ICacheService
{
    private readonly IDistributedCache _cache = cache;
    
    public async Task<T?> GetDataAsync<T>(string key)
    {
        var data = await _cache.GetStringAsync(key);
        if (data is null) 
            return default;
        
        return JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetDataAsync<T>(string key, T data)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(300)
        };
        
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(data), options);
    }

    public async Task RemoveDataAsync<T>(string key)
    {
        await _cache.RemoveAsync(key);
    }
}