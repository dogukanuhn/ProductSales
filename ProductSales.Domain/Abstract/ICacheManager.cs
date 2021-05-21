using System;

namespace ProductSales.Domain.Abstract
{
    public interface ICacheManager
    {
        string TryGet(string cacheKey);
        void Set(string cacheKey, string data, TimeSpan timeout);
        void Set(string cacheKey, string data);


        void Remove(string cacheKey);
    }
}
