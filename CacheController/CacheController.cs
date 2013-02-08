using System;
using System.Runtime.Caching;
using System.Linq;

namespace CacheController
{
    public class CacheController
    {
        public static object Cache(string key, int cacheTimeMilliSeconds, Delegate result, params object[] args)
        {
            var cache = MemoryCache.Default;

            if (cache[key] == null)
            {
                object cacheValue;
                try
                {
                    cacheValue = result.DynamicInvoke(args);
                }
                catch (Exception e)
                {

                    var message = e.InnerException.GetBaseException().Message;
                    throw new Exception(message);

                }
                if (cacheValue != null)
                {
                    cache.Set(key, cacheValue, DateTimeOffset.Now.AddMilliseconds(cacheTimeMilliSeconds));
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
    }
}
