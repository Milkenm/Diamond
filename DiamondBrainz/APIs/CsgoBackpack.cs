using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Schemes.CsgoBackpack;

using Newtonsoft.Json;

using ScriptsLibV2.Extensions;
using ScriptsLibV2.Util;

using static Diamond.API.Utils;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API.APIs
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

		private readonly DiamondDatabase _database;

		private readonly Dictionary<Currency, CsgoBackpackItemsList> _itemsMap = new Dictionary<Currency, CsgoBackpackItemsList>();
		private readonly Dictionary<string, List<CsgoItemMatchInfo>> _searchCacheMap = new Dictionary<string, List<CsgoItemMatchInfo>>();

		public CsgoBackpack(DiamondDatabase database)
		{
			this._database = database;
		}

		public async Task LoadItems()
		{
			long currentUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			foreach (Currency currency in Enum.GetValues(typeof(Currency)))
			{
				// Attempt to load from database
				string key = $"CSGO_ItemsList_{currency.ToString().ToUpper()}";
				CacheRecord cacheRecord = this._database.Cache.Where(cr => cr.Key == key).FirstOrDefault();
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
						this._database.Cache.Add(new CacheRecord()
						{
							Key = key,
							Value = itemsListJson.ToByteArray(),
							UpdatedAt = currentUnix,
						});
					}
					this._database.SaveChanges();
				}
				CsgoBackpackItemsList itemsList = JsonConvert.DeserializeObject<CsgoBackpackItemsList>(itemsListJson);
				if (!itemsList.Success)
				{
					Debug.WriteLine($"Failed to load items for currency '{currency.ToString().ToUpper()}'.");
					continue;
				}
				foreach (KeyValuePair<string, CsgoItemInfo> item in itemsList.ItemsList)
				{
					if (item.Value.Name.Contains("&#39") || item.Value.Name.Contains("%27"))
					{
						item.Value.Name = item.Value.Name.Replace("&#39", "'").Replace("%27", "'");
						itemsList.ItemsList[item.Key] = item.Value;
					}
				}
				// Store the items in the database
				foreach (KeyValuePair<string, CsgoItemInfo> item in itemsList.ItemsList)
				{
					DbCsgoItem newItem = new DbCsgoItem()
					{
						ClassId = Convert.ToInt64(item.Value.ClassID),
						Currency = currency,
						FirstSaleDateUnix = item.Value.FirstSaleDate,
						IconUrl = item.Value.IconUrl,
						Name = item.Value.Name,
						RarityHexColor = item.Value.RarityHexColor,
					};

					if (item.Value.Price != null)
					{
						foreach (KeyValuePair<string, CsgoItemPriceInfo> priceInfo in item.Value.Price)
						{
							DbCsgoItemPrice newPrice = new DbCsgoItemPrice()
							{
								Average = priceInfo.Value.Average,
								HighestPrice = priceInfo.Value.HighestPrice,
								LowestPrice = priceInfo.Value.LowestPrice,
								Median = priceInfo.Value.Median,
								Sold = Convert.ToInt64(priceInfo.Value.Sold.IsEmpty() ? 0 : priceInfo.Value.Sold),
								StandardDeviation = float.Parse(priceInfo.Value.StandardDeviation.Replace('.', ',')),
							};

							switch (priceInfo.Key)
							{
								case "24_hours":
									{
										newItem.Price24Hours = newPrice;
									}
									break;
								case "7_days":
									{
										newItem.Price7Days = newPrice;
									}
									break;
								case "30_days":
									{
										newItem.Price30Days = newPrice;
									}
									break;
								case "all_time":
									{
										newItem.PriceAllTime = newPrice;
									}
									break;
							}
						}
					}
					this._database.Add(newItem);
				}
				this._database.SaveChanges();
			}
		}

		public async Task<List<SearchMatchInfo<CsgoItemInfo>>> Search(string search, Currency currency) => await Utils.Search(this._itemsMap[currency].ItemsList, search);

		public List<CsgoItemMatchInfo> SearchItems(string searchItemName, Currency currency)
		{
			if (!this._itemsMap.ContainsKey(currency))
			{
				return null;
			}

			List<CsgoItemMatchInfo> bestMatches = new List<CsgoItemMatchInfo>();
			if (this._searchCacheMap.ContainsKey(searchItemName))
			{
				bestMatches = this._searchCacheMap[searchItemName];
			}
			else
			{
				foreach (CsgoItemInfo item in this._itemsMap[currency].ItemsList.Values)
				{
					string itemName = item.Name.ToLower().Trim();

					double matches = 0;
					foreach (string word in searchItemName.Split(" "))
					{
						if (itemName.Replace(" ", "").Contains(word))
						{
							matches++;
						}
					}
					matches *= SUtils.CalculateLevenshteinSimilarity(itemName, searchItemName);
					if (matches == 0)
					{
						continue;
					}

					CsgoItemMatchInfo bestMatch = this.GetBestMatch(bestMatches);
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
				if (!this._searchCacheMap.ContainsKey(searchItemName))
				{
					this._searchCacheMap.Add(searchItemName, bestMatches);
				}
				return bestMatches;
			}

			return null;
		}

		public void ClearCache() => this._searchCacheMap.Clear();

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
			this.CsgoItem = csgoItem;
			this.Match = match;
		}
	}
}
