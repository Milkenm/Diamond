using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Schemes.CsgoBackpack;
using Diamond.API.Util;

using Newtonsoft.Json;

using ScriptsLibV2.Extensions;
using ScriptsLibV2.Util;

using Utils = Diamond.API.Util.Utils;

namespace Diamond.API.APIs
{

	public delegate void CsgoItemsStateChanged();

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

		private readonly Dictionary<string, List<SearchMatchInfo<DbCsgoItem>>> _searchCacheMap = new Dictionary<string, List<SearchMatchInfo<DbCsgoItem>>>();

		public bool AreItemsLoaded { get; private set; }

		public event CsgoItemsStateChanged OnCheckingForUpdate;
		public event CsgoItemsStateChanged OnUpdateCancelled;
		public event CsgoItemsStateChanged OnUpdateStart;
		public event CsgoItemsStateChanged OnUpdateEnd;

		public Task LoadItems(bool forceUpdate = false)
		{
			return Task.Run(async () =>
			{
				OnCheckingForUpdate?.Invoke();
				using DiamondContext db = new DiamondContext();

				this.AreItemsLoaded = false;
				long currentUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

				if (!forceUpdate)
				{
					Debug.WriteLine("Checking last refresh unix...");
					// Check if items need to be updated
					long lastUpdate = Convert.ToInt64(db.GetSetting(DiamondContext.ConfigSetting.CsgoItemsLoadUnix));
					if (lastUpdate + (KEEP_RESULTS_FOR_MINUTES * 60) >= currentUnix)
					{
						this.AreItemsLoaded = true;
						OnUpdateCancelled?.Invoke();
						return;
					}
				}
				else
				{
					Debug.WriteLine("Forcing CS:GO items update...");
				}

				OnUpdateStart?.Invoke();
				Debug.WriteLine("Clearing database...");
				// Clear database and items map
				db.ClearTable("CsgoItemPrices");
				db.ClearTable("CsgoItems");
				this._csgoItemsMap?.Clear();

				Dictionary<string, DbCsgoItem> createdItems = new Dictionary<string, DbCsgoItem>();

				foreach (Currency currency in Enum.GetValues(typeof(Currency)))
				{
					// Attempt to load from database
					string key = $"CSGO_ItemsList_{currency.ToString().ToUpper()}";
					string? itemsListJson = null;
					// Download from API
					Debug.WriteLine("Downloading JSON for currency: " + currency.ToString());
					itemsListJson = RequestUtils.GetAsync($"http://csgobackpack.net/api/GetItemsList/v2/?currency={currency.ToString().ToLower()}").GetAwaiter().GetResult();
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

					int i = 0;
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
							_ = db.CsgoItems.Add(newItem);
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
								_ = db.CsgoItemPrices.Add(newPrice);
							}
						}
						i++;
						if (i % 3000 == 0)
						{
							Debug.WriteLine($"Saving to database");
							_ = await db.SaveChangesAsync();
							Debug.WriteLine($"Saved {i} items");
						}
					}
				}
				await db.SetSettingAsync(DiamondContext.ConfigSetting.CsgoItemsLoadUnix, currentUnix);
				int remainingRecords = await db.SaveChangesAsync();
				Debug.WriteLine($"Saved the remaining {remainingRecords} records.");
				Debug.WriteLine("Done!");

				this.AreItemsLoaded = true;
				OnUpdateEnd?.Invoke();
			});
		}

		public List<DbCsgoItemPrice> GetItemPrices(DbCsgoItem csgoItem, Currency currency)
		{
			using DiamondContext db = new DiamondContext();

			return db.CsgoItemPrices.Where(itemPrice => itemPrice.Currency == currency && itemPrice.Item == csgoItem).ToList();
		}

		private Dictionary<string, DbCsgoItem> _csgoItemsMap;

		public List<SearchMatchInfo<DbCsgoItem>> SearchItem(string search)
		{
			using DiamondContext db = new DiamondContext();

			// Generate items map
			if (this._csgoItemsMap == null || this._csgoItemsMap.Count == 0)
			{
				Debug.WriteLine("Generating items map...");
				this._csgoItemsMap = new Dictionary<string, DbCsgoItem>();
				foreach (DbCsgoItem item in db.CsgoItems)
				{
					this._csgoItemsMap.Add(item.Name, item);
				}
				Debug.WriteLine("Generation finished.");
			}

			Debug.WriteLine("Searching...");
			List<SearchMatchInfo<DbCsgoItem>> searchResults = Utils.Search(this._csgoItemsMap, search);
			Debug.WriteLine("Search finished.");

			return searchResults;
		}

		public void ClearCache() => this._searchCacheMap.Clear();
	}

	public enum Currency
	{
		BRL,
		EUR,
		USD,
		JPY,
	}
}
