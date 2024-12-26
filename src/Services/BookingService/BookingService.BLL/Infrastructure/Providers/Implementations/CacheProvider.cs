using BookingService.BLL.Infrastructure.Providers.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BookingService.BLL.Infrastructure.Providers.Implementations;

public class CacheProvider : ICacheProvider
{
    private readonly IDistributedCache _cache;

    public CacheProvider(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetCachedDataAsync<T>(string cacheKey, CancellationToken cancellationToken)
    {
        var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedData is not null)
        {
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        return default;
    }

    public async Task SetCacheDataAsync<T>(string cacheKey, T data, TimeSpan ttl, CancellationToken cancellationToken)
    {
        var cacheData = JsonConvert.SerializeObject(data);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = ttl
        };
        
        await _cache.SetStringAsync(cacheKey, cacheData, options, cancellationToken);
    }

    public async Task RemoveCacheDataAsync(string cacheKey, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(cacheKey, cancellationToken);
    }
}