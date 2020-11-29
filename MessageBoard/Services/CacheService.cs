using Microsoft.Extensions.Caching.Memory;
using MessageBoard.API.Entities;
using MessageBoard.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;

        public CacheService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }

            if (this.cache.TryGetValue(key, out var result))
            {
                return (T)result;
            }

            return default(T);
        }

        public void Store<T>(string key, T cacheObject)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (cacheObject == null)
            {
                throw new ArgumentNullException(nameof(cacheObject));
            }

            this.cache.Set(key, cacheObject);
        }
    }
}
