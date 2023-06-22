using System;
using System.Collections.Generic;

namespace Diamond.API.Helpers
{
    public class Cache<T>
    {
        private readonly Dictionary<string, CacheRecord> _cache = new Dictionary<string, CacheRecord>();

        public void CacheValue(string key, T value, ulong keepForSeconds)
        {
            CacheRecord cacheRecord = new CacheRecord(value, keepForSeconds);
            _ = _cache.Remove(key);
            _cache.Add(key, cacheRecord);
        }

        public T? GetCachedValue(string key)
        {
            if (_cache.ContainsKey(key))
            {
                CacheRecord cachedRecord = _cache[key];
                if (cachedRecord.IsValid())
                {
                    return cachedRecord.Value;
                }
                else
                {
                    _ = _cache.Remove(key);
                }
            }
            return default;
        }

        public void ClearCache()
        {
            _cache.Clear();
        }

        private class CacheRecord
        {
            private readonly ulong _keepFor;
            private readonly ulong _cachedAt;

            public CacheRecord(T value, ulong cacheKeepSeconds)
            {
                _keepFor = cacheKeepSeconds;
                _cachedAt = (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            public T Value { get; set; }

            public bool IsValid()
            {
                return _cachedAt + _keepFor >= (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
        }
    }
}