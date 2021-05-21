using Microsoft.Extensions.Caching.Distributed;

using ProductSales.Domain.Abstract;
using System;
using System.Text;

namespace ProductSales.Infrastructure.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        public string TryGet(string cacheKey)
        {
            var cachedData = _distributedCache.GetString(cacheKey);
            return cachedData;
        }

        public void Set(string cacheKey, string data, TimeSpan timeout)
        {
            _distributedCache.SetStringAsync(cacheKey, data, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeout
            });

        }
        public void Set(string cacheKey, string data)
        {
            Set(cacheKey, data, TimeSpan.MaxValue);
        }

        public void Remove(string cacheKey)
        {
            _distributedCache.Remove(cacheKey);
        }
    }
}
