﻿using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace Connect.Core.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T> GetOrAdd<T>(this IDistributedCache distributedCache, Func<Task<T>> action, string key)
        {
            var cached = distributedCache.GetString(key);
            if (string.IsNullOrEmpty(cached))
            {
                cached = SerializeObject(await action());
                await distributedCache.SetStringAsync(cached, key);
            }
            return DeserializeObject<T>(cached);
        }

        public static T GetOrAddSync<T>(this IDistributedCache distributedCache, Func<T> action, string key)
        {
            var cached = distributedCache.GetString(key);
            if (string.IsNullOrEmpty(cached))
            {
                cached = SerializeObject(action());
                distributedCache.SetString(cached, key);
            }
            return DeserializeObject<T>(cached);
        }
    }
}
