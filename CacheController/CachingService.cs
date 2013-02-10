using System;
using System.Runtime.Caching;
using System.Linq;
using Cache;

[assembly: CLSCompliant(true)]

namespace CacheController
{
    public static class CachingService
    {
        public static object Cache(string key, int cacheTimeMilliseconds, Delegate result, params object[] args)
        {
            var cache = MemoryCache.Default;

            if (cache[key] == null)
            {
                object cacheValue;
                try
                {
                    if (result == null)
                        return null;
                    cacheValue = result.DynamicInvoke(args);
                }
                catch (Exception e)
                {

                    var message = e.InnerException.GetBaseException().Message;
                    throw new CacheDelegateMethodException(message);

                }
                if (cacheValue != null)
                {
                    cache.Set(key, cacheValue, DateTimeOffset.Now.AddMilliseconds(cacheTimeMilliseconds));
                }
            }
            return cache[key];
        }

        public static void DeleteCache(string key)
        {
            MemoryCache.Default.Remove(key);
        }
        
        public static void DeleteAll()
        {
            MemoryCache.Default.Select(kvp => kvp.Key).ToList().ForEach((x) => MemoryCache.Default.Remove(x));            
        }

        public static void UpdateCacheForKey(string key, int cacheMilliseconds, object result)
        {
            var cache = MemoryCache.Default;

            if (cache[key] != null)
            {
                DeleteCache(key);
            }
            cache.Set(key, result, DateTimeOffset.Now.AddMilliseconds(cacheMilliseconds));
        }
    }
}
