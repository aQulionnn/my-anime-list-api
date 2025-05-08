namespace FranchiseService.Application.Services;

public interface ICacheService
{
    Task<T?> GetDataAsync<T>(string key);
    Task SetDataAsync<T>(string key, T data, TimeSpan ttl);
    Task RemoveDataAsync<T>(string key);
}