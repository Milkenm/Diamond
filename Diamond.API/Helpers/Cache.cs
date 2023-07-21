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
			_ = this._cache.Remove(key);
			this._cache.Add(key, cacheRecord);
		}

		public T? GetCachedValue(string key)
		{
			if (this._cache.ContainsKey(key))
			{
				CacheRecord cachedRecord = this._cache[key];
				if (cachedRecord.IsValid())
				{
					return cachedRecord.Value;
				}
				else
				{
					_ = this._cache.Remove(key);
				}
			}
			return default;
		}

		public void ClearCache()
		{
			this._cache.Clear();
		}

		private class CacheRecord
		{
			private readonly ulong _keepFor;
			private readonly ulong _cachedAt;

			public CacheRecord(T value, ulong cacheKeepSeconds)
			{
				this._keepFor = cacheKeepSeconds;
				this._cachedAt = (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			}

			public T Value { get; set; }

			public bool IsValid()
			{
				return this._cachedAt + this._keepFor >= (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			}
		}
	}
}