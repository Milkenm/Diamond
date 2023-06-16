using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.Data;

namespace Diamond.API.Util.APIManager
{
	public abstract class SearchableAPIManager<T> : APIManager<T>
	{
		private bool _cacheSearches { get; set; }

		private readonly Cache<List<SearchMatchInfo<T>>> _searchCache = new Cache<List<SearchMatchInfo<T>>>();

		public SearchableAPIManager(ConfigSetting dbUnixConfigSetting, ulong keepResultsForSeconds, string[] tablesName)
			: base(dbUnixConfigSetting, keepResultsForSeconds, tablesName)
		{ }

		public async Task<List<SearchMatchInfo<T>>> SearchItemAsync(string search)
		{
			// Lock thread while pokémons are not loaded
			while (!this.AreItemsLoaded)
			{
				await Task.Delay(250);
			}

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
				this._searchCache.CacheValue(search, searchResults, this.KeepResultsForSeconds);
			}

			return searchResults;
		}

		public void ClearCache()
		{
			this._searchCache.ClearCache();
		}
	}
}
