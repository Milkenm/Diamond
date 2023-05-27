using System;
using System.Collections.Generic;

namespace Diamond.API.Util;
public class Cache<T>
{
    private Dictionary<string, CacheRecord> _cache = new Dictionary<string, CacheRecord>();

    public void CacheValue(string key, T value, long keepForSeconds)
    {
        CacheRecord cacheRecord = new CacheRecord(value, keepForSeconds);
        _cache.Remove(key);
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
                _cache.Remove(key);
            }
        }
        return default;
    }

    public class CacheRecord
    {
        private long _keepFor;
        private long _cachedAt;

        public CacheRecord(T value, long cacheKeepSeconds)
        {
            _keepFor = cacheKeepSeconds;
            _cachedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public T Value { get; set; }

        public bool IsValid()
        {
            return _cachedAt + _keepFor >= DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
