using System;
using System.Collections.Generic;

namespace CacheController
{
    public interface ICachingService
    {
        T GetCacheValue<T>(string key);
        object GetCacheValue(string key);

        void UpdateCacheForKey(string key, long cacheTimeMilliseconds, object cachedItem);
        void UpdateCacheForKey(string key, long cacheTime, CacheTimeType cacheTimeType, object cachedItem);

        object Cache(string key, long cacheTimeMilliseconds, Delegate result, params object[] args);
        object Cache(string key, long cacheTime, CacheTimeType cacheTimeType, Delegate result, params object[] args);
        T Cache<T>(string key, long cacheTime, CacheTimeType cacheTimeType, Delegate result, params object[] args);

        void DeleteCache(string key);

        void DeleteAll();

        IEnumerable<string> CacheKeys { get; }
    }
}