using Diamond.API.APIs;
using Diamond.API.Schems;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using ScriptsLibV2.Extensions;
using ScriptsLibV2.Util;

internal class Program
{
	private static void Main(string[] args)
	{
		RefreshCsgoItems();
	}

	private static readonly List<string> _addedIds = new List<string>();
	private static readonly List<CsgoRarity> _addedRarities = new List<CsgoRarity>();

	private static void RefreshCsgoItems()
	{
		if (GetItemsList(out CsgoItemsList? csgoItemsEurList, out CsgoItemsList? csgoItemsUsdList, out CsgoItemsList? csgoItemsBrlList) && csgoItemsEurList != null)
		{
			using (CsgoDatabase? database = new CsgoDatabase())
			{
				ClearDatabase(database);


				CreateItems(csgoItemsEurList, csgoItemsUsdList, csgoItemsBrlList, database);
			}
		}
	}

	private static bool GetItemsList(out CsgoItemsList? csgoItemsEurList, out CsgoItemsList? csgoItemsUsdList, out CsgoItemsList? csgoItemsBrlList)
	{
		try
		{
			string itemsEurResponse = RequestUtils.Get("http://csgobackpack.net/api/GetItemsList/v2/?currency=eur");
			csgoItemsEurList = JsonConvert.DeserializeObject<CsgoItemsList?>(itemsEurResponse);

			string itemsUsdResponse = RequestUtils.Get("https://csgobackpack.net/api/GetItemsList/v2/?currency=usd&no_details=true");
			csgoItemsUsdList = JsonConvert.DeserializeObject<CsgoItemsList?>(itemsUsdResponse);

			string itemsBrlResponse = RequestUtils.Get("https://csgobackpack.net/api/GetItemsList/v2/?currency=brl&no_details=true");
			csgoItemsBrlList = JsonConvert.DeserializeObject<CsgoItemsList?>(itemsBrlResponse);

			return true;
		}
		catch
		{
			csgoItemsEurList = csgoItemsUsdList = csgoItemsBrlList = null;
			return false;
		}
	}

	private static void ClearDatabase(CsgoDatabase database)
	{
		database.Database.ExecuteSqlRaw("DELETE FROM Prices");
		database.Database.ExecuteSqlRaw("DELETE FROM Items");
		database.Database.ExecuteSqlRaw("DELETE FROM Rarities");
	}

	private static void CreateItems(CsgoItemsList? csgoItemsEurList, CsgoItemsList? csgoItemsUsdList, CsgoItemsList? csgoItemsBrlList, CsgoDatabase database)
	{
		foreach (CsgoItemInfo item in csgoItemsEurList.ItemsList.Values)
		{
			if (_addedIds.Contains(item.ClassID))
			{
				continue;
			}
			_addedIds.Add(item.ClassID);

			CsgoItem newItem = new CsgoItem
			{
				Id = Convert.ToInt64(item.ClassID),
				Name = item.Name.Replace("&#39", "\""),
				IconUrl = item.IconUrl,
				Type = item.Type,
			};
			if (!item.Rarity.IsEmpty())
			{
				CreateRarities(_addedRarities, item, newItem);
			}
			database.Add(newItem);

			if (item.Price != null)
			{
				CreatePrices(csgoItemsEurList, database, item, newItem);
				CreatePrices(csgoItemsUsdList, database, item, newItem);
				CreatePrices(csgoItemsBrlList, database, item, newItem);
			}
		}
		database.SaveChanges();
	}

	private static void CreatePrices(CsgoItemsList? itemsEur, CsgoDatabase database, CsgoItemInfo item, CsgoItem addItem)
	{
		List<string> timesList = new List<string>() { "24_hours", "7_days", "30_days", "all_time", };
		List<string> currenciesList = new List<string>() { "eur", "usd", "brl" };

		foreach (string time in timesList)
		{
			if (item.Price.ContainsKey(time))
			{
				CsgoPrice priceTime = new CsgoPrice()
				{
					TargetItem = addItem,
					Currency = itemsEur.Currency,
					Average = item.Price[time].Average,
					Median = item.Price[time].Median,
					Sold = Convert.ToInt32(!item.Price[time].Sold.IsEmpty() ? item.Price[time].Sold : "0"),
					StandardDeviation = Convert.ToDouble(item.Price[time].StandardDeviation.Replace(".", ",")),
					LowestPrice = item.Price[time].LowestPrice,
					HighestPrice = item.Price[time].HighestPrice,
				};
				database.Add(priceTime);
			}
		}
	}

	private static void CreateRarities(List<CsgoRarity> addedRarities, CsgoItemInfo item, CsgoItem newItem)
	{
		CsgoRarity? rarity = addedRarities.Find(r => r.Name == item.Rarity);
		if (rarity == null)
		{
			rarity = new CsgoRarity()
			{
				Name = item.Rarity,
				Color = item.RarityHexColor,
			};
			addedRarities.Add(rarity);
		}
		newItem.Rarity = rarity;
	}
}