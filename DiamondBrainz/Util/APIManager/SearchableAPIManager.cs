using System.Collections.Generic;

namespace Diamond.API.Util.APIManager
{
	public abstract class SearchableAPIManager<T> : APIManager<T>
	{
		private bool _cacheSearches { get; set; }
		private ulong _keepSearchCacheForSeconds { get; set; }

		private readonly Cache<List<SearchMatchInfo<T>>> _searchCache = new Cache<List<SearchMatchInfo<T>>>();

		public SearchableAPIManager(ulong keepSearchCacheForSeconds)
		{
			this._keepSearchCacheForSeconds = keepSearchCacheForSeconds;
		}

		public List<SearchMatchInfo<T>> SearchItem(string search)
		{
			// Get the value from the search cache
			if (this._cacheSearches)
			{
				List<SearchMatchInfo<T>> cacheValue = this._searchCache.GetCachedValue(search);
				if (cacheValue != null)
				{
					return cacheValue;
				}
			}

			// Generate items map
			if (this._itemsMap.Count == 0)
			{
				this.LoadItemsMap(this._itemsMap);
			}

			// Search item
			List<SearchMatchInfo<T>> searchResults = Utils.Search(this._itemsMap, search);

			// Save to cache (if enabled)
			if (this._cacheSearches)
			{
				this._searchCache.CacheValue(search, searchResults, this._keepSearchCacheForSeconds);
			}

			return searchResults;
		}

		public void ClearCache()
		{
			this._searchCache.ClearCache();
		}
	}
}
