namespace AnimeSeries.Application.Services;

public interface ICacheService
{
    Task<T?> GetDataAsync<T>(string key);
    Task SetDataAsync<T>(string key, T data);
    Task RemoveDataAsync<T>(string key); 
}