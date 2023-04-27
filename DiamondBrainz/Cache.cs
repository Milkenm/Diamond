using System;
using System.Collections.Generic;

namespace Diamond.API;
public class Cache<T>
{
	private Dictionary<string, CacheRecord2> _cache = new Dictionary<string, CacheRecord2>();

	public void CacheValue(string key, T value, long keepForSeconds)
	{
		CacheRecord2 cacheRecord = new CacheRecord2(value, keepForSeconds);
		_cache.Remove(key);
		_cache.Add(key, cacheRecord);
	}

	public T? GetCachedValue(string key)
	{
		if (_cache.ContainsKey(key))
		{
			CacheRecord2 cachedRecord = _cache[key];
			if (cachedRecord.IsValid())
			{
				return cachedRecord.Value;
			}
		}
		return default;
	}

	public class CacheRecord2
	{
		private long _keepFor;
		private long _cachedAt;

		public CacheRecord2(T value, long cacheKeepSeconds)
		{
			_keepFor = cacheKeepSeconds;
			_cachedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}

		public T Value { get; set; }

		public bool IsValid()
		{
			return (_cachedAt + _keepFor) >= DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}
	}
}
