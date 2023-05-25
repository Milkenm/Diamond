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

namespace Diamond.API.APIs
{
	public class CsgoBackpack
	{
		private const long KEEP_RESULTS_FOR_MINUTES = 60;

		public static readonly Dictionary<Currency, string> CurrencySymbols = new Dictionary<Currency, string>()
	{
		{ Currency.BRL, "R$"},
		{ Currency.EUR, "€" },
		{ Currency.USD, "$" },
		{ Currency.JPY, "¥" },
	};

		private readonly DiamondDatabase _database;

		public CsgoBackpack(DiamondDatabase database)
		{
			this._database = database;
		}

		private readonly Dictionary<string, List<SearchMatchInfo<DbCsgoItem>>> _searchCacheMap = new Dictionary<string, List<SearchMatchInfo<DbCsgoItem>>>();

		public async Task LoadItemsAsync()
		{
			await Task.Run(async () =>
			{
				Debug.WriteLine("Checking last refresh unix.");
				// Check if items need to be refreshed
				long currentUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
				long lastUpdate = Convert.ToInt64(this._database.GetSetting(DiamondDatabase.ConfigSetting.CsgoItemsLoadUnix));
				if (lastUpdate + (KEEP_RESULTS_FOR_MINUTES * 60) >= currentUnix)
				{
					return;
				}

				Debug.WriteLine("Clearing database");
				// Clear database and items map
				this._database.ClearTable("CsgoItemPrices");
				this._database.ClearTable("CsgoItems");
				this._csgoItemsMap?.Clear();

				Dictionary<string, DbCsgoItem> createdItems = new Dictionary<string, DbCsgoItem>();

				foreach (Currency currency in Enum.GetValues(typeof(Currency)))
				{
					// Attempt to load from database
					string key = $"CSGO_ItemsList_{currency.ToString().ToUpper()}";
					/*CacheRecord cacheRecord = _database.Cache.Where(cr => cr.Key == key).FirstOrDefault();*/
					string? itemsListJson = null;
					/*if (cacheRecord != null)
					{
						if (cacheRecord.UpdatedAt >= currentUnix - 21600L) // 6 hours
						{
							itemsListJson = cacheRecord.Value.ToObject<string>();
						}
					}*/
					/*if (itemsListJson == null)
					{*/
					// Download from API
					Debug.WriteLine("Downloading JSON for currency: " + currency.ToString());
					itemsListJson = await RequestUtils.GetAsync($"http://csgobackpack.net/api/GetItemsList/v2/?currency={currency.ToString().ToLower()}");
					/*	if (cacheRecord != null)
						{
							cacheRecord.Value = itemsListJson.ToByteArray();
							cacheRecord.UpdatedAt = currentUnix;
						}
						else
						{
							_database.Cache.Add(new CacheRecord()
							{
								Key = key,
								Value = itemsListJson.ToByteArray(),
								UpdatedAt = currentUnix,
							});
						}
						await _database.SaveAsync();
					}*/
					Debug.WriteLine("Deserializing");
					CsgoBackpackItemsList itemsList = JsonConvert.DeserializeObject<CsgoBackpackItemsList>(itemsListJson);
					if (!itemsList.Success)
					{
						Debug.WriteLine($"Failed to load items for currency '{currency.ToString().ToUpper()}'.");
						continue;
					}
					Debug.WriteLine("Fixing item names");
					foreach (KeyValuePair<string, CsgoItemInfo> item in itemsList.ItemsList)
					{
						if (item.Value.Name.Contains("&#39") || item.Value.Name.Contains("%27"))
						{
							item.Value.Name = item.Value.Name.Replace("&#39", "'").Replace("%27", "'");
							itemsList.ItemsList[item.Key] = item.Value;
						}
					}

					// Store the items in the database
					Debug.WriteLine("Storing items in the database");
					foreach (KeyValuePair<string, CsgoItemInfo> item in itemsList.ItemsList)
					{
						DbCsgoItem csgoItem;
						if (!createdItems.ContainsKey(item.Value.Name))
						{
							DbCsgoItem newItem = new DbCsgoItem()
							{
								ClassId = Convert.ToInt64(item.Value.ClassID),
								FirstSaleDateUnix = item.Value.FirstSaleDate,
								IconUrl = item.Value.IconUrl,
								Name = item.Value.Name,
								RarityHexColor = item.Value.RarityHexColor,
							};
							_ = this._database.CsgoItems.Add(newItem);
							createdItems.Add(item.Value.Name, newItem);
							csgoItem = newItem;
						}
						else
						{
							csgoItem = createdItems[item.Value.Name];
						}

						if (item.Value.Price != null)
						{
							foreach (KeyValuePair<string, CsgoItemPriceInfo> priceInfo in item.Value.Price)
							{
								DbCsgoItemPrice newPrice = new DbCsgoItemPrice()
								{
									Item = csgoItem,
									Epoch = priceInfo.Key,
									Currency = currency,
									Average = priceInfo.Value.Average,
									HighestPrice = priceInfo.Value.HighestPrice,
									LowestPrice = priceInfo.Value.LowestPrice,
									Median = priceInfo.Value.Median,
									Sold = Convert.ToInt64(priceInfo.Value.Sold.IsEmpty() ? 0 : priceInfo.Value.Sold),
									StandardDeviation = float.Parse(priceInfo.Value.StandardDeviation.Replace('.', ',')),
								};
								_ = this._database.CsgoItemPrices.Add(newPrice);
							}
						}
					}
					Debug.WriteLine("Saving database changes");
					await this._database.SaveAsync();
					Debug.WriteLine("Saved");
				}
				await this._database.SetSetting(DiamondDatabase.ConfigSetting.CsgoItemsLoadUnix, currentUnix);
			});
		}

		public List<DbCsgoItemPrice> GetItemPrices(DbCsgoItem csgoItem, Currency currency)
		{
			using (DiamondDatabase db = new DiamondDatabase())
			{
				return this._database.CsgoItemPrices.Where(itemPrice => itemPrice.Currency == currency && itemPrice.Item == csgoItem).ToList();
			}
		}

		private Dictionary<string, DbCsgoItem> _csgoItemsMap;

		public async Task<List<SearchMatchInfo<DbCsgoItem>>> SearchItemAsync(string search)
		{
			// Generate items map
			if (this._csgoItemsMap == null)
			{
				Debug.WriteLine("Generating items map...");
				this._csgoItemsMap = new Dictionary<string, DbCsgoItem>();
				using (DiamondDatabase db = new DiamondDatabase())
				{
					foreach (DbCsgoItem item in this._database.CsgoItems)
					{
						this._csgoItemsMap.Add(item.Name, item);
					}
				}
				Debug.WriteLine("Generation finished.");
			}

			Debug.WriteLine("Searching...");
			List<SearchMatchInfo<DbCsgoItem>> searchResults = await Utils.Search(this._csgoItemsMap, search);
			Debug.WriteLine("Search finished.");

			return searchResults;
		}

		public void ClearCache()
		{
			this._searchCacheMap.Clear();
		}
	}

	public enum Currency
	{
		BRL,
		EUR,
		USD,
		JPY,
	}
}