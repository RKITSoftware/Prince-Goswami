using System;
using System.Web.Caching;

/// <summary>
/// Service for caching and retrieving data.
/// </summary>
public class CacheManager
{
    private Cache _cache;

    /// <summary>
    /// Constructor for initializing the CacheManager.
    /// </summary>
    public CacheManager()
    {
        _cache = new Cache();
    }

    /// <summary>
    /// Retrieves cached data or fetches new data if not cached.
    /// </summary>
    /// <typeparam name="T">The type of data to cache.</typeparam>
    /// <param name="functionName">The name of the function used as the cache key.</param>
    /// <param name="function">The function to fetch new data if not cached.</param>
    /// <returns>The cached data or newly fetched data.</returns>
    public object GetCachedResponse<T>(string functionName, Func<T> function)
    {
        string cacheKey = functionName;

        // Try to get data from cache
        object cachedData = _cache.Get(cacheKey) as object;
        if (cachedData != null)
        {
            return cachedData;
        }

        // If not in cache, fetch the data
        object newData = function();

        // Cache the data for a short duration (e.g., 15 seconds)
        _cache.Insert(cacheKey, newData, null, DateTime.Now.AddSeconds(15), Cache.NoSlidingExpiration);

        return newData;
    }
}
