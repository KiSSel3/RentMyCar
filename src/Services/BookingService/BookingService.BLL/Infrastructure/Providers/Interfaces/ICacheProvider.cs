namespace BookingService.BLL.Infrastructure.Providers.Interfaces;

public interface ICacheProvider
{
    Task<T?> GetCachedDataAsync<T>(string cacheKey, CancellationToken cancellationToken = default);
    Task SetCacheDataAsync<T>(string cacheKey, T data, TimeSpan ttl, CancellationToken cancellationToken = default);
    Task RemoveCacheDataAsync(string cacheKey, CancellationToken cancellationToken = default);
}