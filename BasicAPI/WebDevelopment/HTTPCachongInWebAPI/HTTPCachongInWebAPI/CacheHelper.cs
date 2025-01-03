using System;
using System.Runtime.Caching;

namespace HTTPCachingInWebAPI
{
    /// <summary>
    /// A helper class for managing in-memory caching using <see cref="MemoryCache"/>.
    /// Provides methods to get, set, and clear cache entries.
    /// </summary>
    public class CacheHelper
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;

        /// <summary>
        /// Retrieves the cached data associated with the specified key.
        /// </summary>
        /// <param name="key">The unique key for the cached data.</param>
        /// <returns>The cached object, or <c>null</c> if the key does not exist in the cache.</returns>
        public static object Get(string key)
        {
            return Cache[key];
        }

        /// <summary>
        /// Stores the specified data in the cache with an expiration time.
        /// </summary>
        /// <param name="key">The unique key for the cached data.</param>
        /// <param name="value">The data to be cached.</param>
        /// <param name="expiration">The duration for which the data should be cached.</param>
        public static void Set(string key, object value, TimeSpan expiration)
        {
            Cache.Set(key, value, DateTimeOffset.Now.Add(expiration));
        }

        /// <summary>
        /// Clears a specific cache entry identified by the given key.
        /// </summary>
        /// <param name="key">The unique key for the cached data to be removed.</param>
        public static void Clear(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Clears all cache entries stored in the cache.
        /// </summary>
        public static void ClearAll()
        {
            foreach (var key in Cache)
            {
                Cache.Remove(key.Key);
            }
        }
    }
}
