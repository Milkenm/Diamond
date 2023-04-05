using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Diamond.API.Schems;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.Stuff
{
	public class CsgoBackpack
	{
		public static readonly Dictionary<Currency, string> CurrencySymbols = new Dictionary<Currency, string>()
		{
			{ Currency.BRL, "R$"},
			{ Currency.EUR, "€" },
			{ Currency.USD, "$" },
			{ Currency.JPY, "¥" },
		};

		private AppFolder _appFolder;

		private readonly Dictionary<Currency, CsgoItemsList> _itemsMap = new Dictionary<Currency, CsgoItemsList>();
		private readonly Dictionary<string, CsgoItemMatchInfo> _searchCacheMap = new Dictionary<string, CsgoItemMatchInfo>();

		public CsgoBackpack(AppFolder appFolder)
		{
			_appFolder = appFolder;
		}

		public async Task LoadItems()
		{
			foreach (Currency currency in Enum.GetValues(typeof(Currency)))
			{
				// Attempt to load from cache folder
				string filePath = $"Cache/CsgoBackpack/ItemsList_{currency.ToString().ToUpper()}.json";
				string itemsListJson = _appFolder.ReadFile(filePath);
				if (itemsListJson == null)
				{
					// Download from API
					itemsListJson = await RequestUtils.GetAsync($"http://csgobackpack.net/api/GetItemsList/v2/?currency={currency.ToString().ToLower()}");
					_appFolder.WriteFile(filePath, itemsListJson);
				}
				CsgoItemsList itemsList = JsonConvert.DeserializeObject<CsgoItemsList>(itemsListJson);
				if (!itemsList.Success)
				{
					Debug.WriteLine($"Failed to load items for currency '{currency.ToString().ToUpper()}'.");
					continue;
				}
				_itemsMap.Add(currency, itemsList);
			}
		}

		public CsgoItemMatchInfo? SearchItem(string searchItemName, Currency currency)
		{
			if (!_itemsMap.ContainsKey(currency)) return null;

			CsgoItemMatchInfo? bestMatch = null;
			if (_searchCacheMap.ContainsKey(searchItemName))
			{
				bestMatch = _searchCacheMap[searchItemName];
			}
			else
			{
				foreach (CsgoItemInfo item in _itemsMap[currency].ItemsList.Values)
				{
					string itemName = item.Name.Replace("&#39", "\"").ToLower().Trim();

					double matches = 0;
					foreach (string word in searchItemName.Split(" "))
					{
						if (itemName.Replace(" ", "").Contains(word))
						{
							matches++;
						}
					}
					matches *= Utils.CalculateLevenshteinSimilarity(itemName, searchItemName);
					if (bestMatch == null || bestMatch.Match < matches)
					{
						bestMatch = new CsgoItemMatchInfo(item, matches);
					}
				}
			}
			if (bestMatch != null)
			{
				if (!_searchCacheMap.ContainsKey(searchItemName))
				{
					_searchCacheMap.Add(searchItemName, bestMatch);
				}
				return bestMatch;
			}

			return null;
		}

		public void ClearCache()
		{
			_searchCacheMap.Clear();
		}
	}

	public enum Currency
	{
		BRL,
		EUR,
		USD,
		JPY,
	}

	public class CsgoItemMatchInfo
	{
		public CsgoItemInfo CsgoItem { get; set; }
		public double Match { get; set; }

		public CsgoItemMatchInfo(CsgoItemInfo csgoItem, double match)
		{
			CsgoItem = csgoItem;
			Match = match;
		}
	}
}
