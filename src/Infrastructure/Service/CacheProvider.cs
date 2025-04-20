using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Service;

public sealed class CacheProvider(IMemoryCache cache) : ICacheProvider
{
    public T GetFromCache<T>(string key) where T : class
    {
        cache.TryGetValue(key, out T cachedResponce);
        return cachedResponce as T;
    }

    public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
    {
        cache.Set(key, value, options);
    }

    public void ClearCache(string key)
    {
        cache.Remove(key);
    }
}