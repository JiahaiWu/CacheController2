using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace CacheController
{
    public class CachingService : ICachingService
    {
        
        public object GetCacheValue(string key)
        {
            var cache = MemoryCache.Default;
            return cache[key];
        }

        public T GetCacheValue<T>(string key)
        {
            return (T)GetCacheValue(key);
        }

        public void UpdateCacheForKey(string key, long cacheTime, CacheTimeType cacheTimeType, object cachedItem)
        {
            var cTime = CacheTime.GetCacheTime(cacheTime, cacheTimeType);
            UpdateCacheForKey(key, cTime, cachedItem);            
        }

        public void UpdateCacheForKey(string key, long cacheTimeMilliseconds, object cachedItem)
        {
            var cache = MemoryCache.Default;

            if (cache[key] != null)
            {
                DeleteCache(key);
            }
            cache.Set(key, cachedItem, DateTimeOffset.Now.AddMilliseconds(cacheTimeMilliseconds));
        }

        public T Cache<T>(string key, long cacheTime, CacheTimeType cacheTimeType, Delegate result, params object[] args)
        {
            var time = CacheTime.GetCacheTime(cacheTime, cacheTimeType);
            return (T)Cache(key, time, result, args);
        }

        public object Cache(string key, long cacheTime, CacheTimeType cacheTimeType, Delegate result, params object[] args)
        {
            var time = CacheTime.GetCacheTime(cacheTime, cacheTimeType);

            return Cache(key, time, result, args);
        }
  
        public object Cache(string key, long cacheTimeMilliseconds, Delegate result, params object[] args)
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

                    if (cacheValue == null)
                        return null;
                }
                catch (Exception e)
                {

                    var message = e.InnerException.GetBaseException().Message;
                    throw new CacheDelegateMethodException(message);

                }                
                cache.Set(key, cacheValue, DateTimeOffset.Now.AddMilliseconds(cacheTimeMilliseconds));
            }
            return cache[key];
        }

        public void DeleteCache(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        public void DeleteAll()
        {
            MemoryCache.Default.Select(kvp => kvp.Key).ToList().ForEach(x => MemoryCache.Default.Remove(x));            
        }

        public IEnumerable<string> CacheKeys
        {
            get { return MemoryCache.Default.Select(kvp => kvp.Key); }
        }


        
    }
}
