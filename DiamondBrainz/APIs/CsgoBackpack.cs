using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Schems.CsgoBackpack;

using Newtonsoft.Json;

using ScriptsLibV2.Extensions;
using ScriptsLibV2.Util;

using SUtils = ScriptsLibV2.Util.Utils;

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

		private DiamondDatabase _diamondDb;

		private readonly Dictionary<Currency, CsgoBackpackItemsList> _itemsMap = new Dictionary<Currency, CsgoBackpackItemsList>();
		private readonly Dictionary<string, List<CsgoItemMatchInfo>> _searchCacheMap = new Dictionary<string, List<CsgoItemMatchInfo>>();

		public CsgoBackpack(DiamondDatabase diamondDb)
		{
			_diamondDb = diamondDb;
		}

		public async Task LoadItems()
		{
			long currentUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			foreach (Currency currency in Enum.GetValues(typeof(Currency)))
			{
				// Attempt to load from database
				string key = $"CSGO_ItemsList_{currency.ToString().ToUpper()}";
				CacheRecord cacheRecord = _diamondDb.Cache.Where(cr => cr.Key == key).FirstOrDefault();
				string? itemsListJson = null;
				if (cacheRecord != null)
				{
					if (cacheRecord.UpdatedAt >= currentUnix - 21600L) // 6 hours
					{
						itemsListJson = cacheRecord.Value.ToObject<string>();
					}
				}
				if (itemsListJson == null)
				{
					// Download from API
					itemsListJson = await RequestUtils.GetAsync($"http://csgobackpack.net/api/GetItemsList/v2/?currency={currency.ToString().ToLower()}");
					if (cacheRecord != null)
					{
						cacheRecord.Value = itemsListJson.ToByteArray();
						cacheRecord.UpdatedAt = currentUnix;
					}
					else
					{
						_diamondDb.Cache.Add(new CacheRecord()
						{
							Key = key,
							Value = itemsListJson.ToByteArray(),
							UpdatedAt = currentUnix,
						});
					}
					_diamondDb.SaveChanges();
				}
				CsgoBackpackItemsList itemsList = JsonConvert.DeserializeObject<CsgoBackpackItemsList>(itemsListJson);
				if (!itemsList.Success)
				{
					Debug.WriteLine($"Failed to load items for currency '{currency.ToString().ToUpper()}'.");
					continue;
				}
				_itemsMap.Add(currency, itemsList);
			}
		}

		public List<CsgoItemMatchInfo> SearchItems(string searchItemName, Currency currency)
		{
			if (!_itemsMap.ContainsKey(currency)) return null;

			List<CsgoItemMatchInfo> bestMatches = new List<CsgoItemMatchInfo>();
			if (_searchCacheMap.ContainsKey(searchItemName))
			{
				bestMatches = _searchCacheMap[searchItemName];
			}
			else
			{
				foreach (CsgoItemInfo item in _itemsMap[currency].ItemsList.Values)
				{
					string itemName = item.Name.Replace("&#39", "'").ToLower().Trim();

					double matches = 0;
					foreach (string word in searchItemName.Split(" "))
					{
						if (itemName.Replace(" ", "").Contains(word))
						{
							matches++;
						}
					}
					matches *= SUtils.CalculateLevenshteinSimilarity(itemName, searchItemName);
					if (matches == 0) continue;
					CsgoItemMatchInfo bestMatch = GetBestMatch(bestMatches);
					if (bestMatches.Count > 0 && matches > bestMatch.Match + 0.35D)
					{
						foreach (CsgoItemMatchInfo matchInfo in bestMatches)
						{
							if (matchInfo.Match < bestMatch.Match - 0.35D)
							{
								bestMatches.Remove(matchInfo);
							}
						}
						bestMatches.Clear();
					}
					if (bestMatches.Count == 0 || (matches >= bestMatch.Match - 0.35D && matches <= bestMatch.Match + 0.35D))
					{
						bestMatches.Add(new CsgoItemMatchInfo(item, matches));
					}
				}
			}
			if (bestMatches.Count > 0)
			{
				if (!_searchCacheMap.ContainsKey(searchItemName))
				{
					_searchCacheMap.Add(searchItemName, bestMatches);
				}
				return bestMatches;
			}

			return null;
		}

		public void ClearCache()
		{
			_searchCacheMap.Clear();
		}

		private CsgoItemMatchInfo GetBestMatch(List<CsgoItemMatchInfo> matches)
		{
			CsgoItemMatchInfo bestMatch = null;
			foreach (CsgoItemMatchInfo matchInfo in matches)
			{
				if (bestMatch == null || bestMatch.Match < matchInfo.Match)
				{
					bestMatch = matchInfo;
				}
			}
			return bestMatch;
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
