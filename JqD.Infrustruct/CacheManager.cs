using System;
using System.Linq;
using System.Runtime.Caching;

namespace JqD.Infrustruct
{
    public static class CacheManager
    {
        private static readonly MemoryCache Cache;
        private const double DefaultExpirationInDay = 1;

        static CacheManager()
        {
            var lockobj = new object();
            lock (lockobj)
            {
                Cache = new MemoryCache("JqDCache");
            }
        }

        public static void Add(string key, object value)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddDays(DefaultExpirationInDay)
            };
            var removeCacheKeys = (from keyValuePair in Cache where keyValuePair.Key.Split('_')[0] == key.Split('_')[0] select keyValuePair.Key).ToList();
            foreach (var removeCacheKey in removeCacheKeys)
            {
                Cache.Remove(removeCacheKey);
            }

            Cache.Add(key, value, policy);

        }

        public static void Add(string key, object value, TimeSpan timeout)
        {
            Cache.Add(key, value, new CacheItemPolicy
            {
                SlidingExpiration = timeout
            });
        }

        public static bool Contains(string key)
        {
            return Cache.Contains(key);
        }
        public static void Set(string key, object value)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddDays(DefaultExpirationInDay)
            };
            Cache.Set(key, value, policy);
        }

        public static T Get<T>(string key)
        {
            if (Cache.Get(key) != null) return (T)Cache.Get(key);
            return default(T);
        }

        public static void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}
