namespace CacheController
{
    public enum CacheTimeType
    {
        Second,
        Minute,
        Hour,
        Milliseconds
    }

    public static class CacheTime
    {
        public static long GetCacheTime(long cacheTime, CacheTimeType cacheTimeType)
        {
            int cacheTimeMultiplier;

            switch (cacheTimeType)
            {
                case CacheTimeType.Second:
                    cacheTimeMultiplier = 1000;
                    break;
                case CacheTimeType.Minute:
                    cacheTimeMultiplier = 60000;
                    break;
                case CacheTimeType.Hour:
                    cacheTimeMultiplier = 3600000;
                    break;
                default:
                    cacheTimeMultiplier = 1;
                    break;
            }

            var result = cacheTime*cacheTimeMultiplier;
            return result;
        }
    }
}