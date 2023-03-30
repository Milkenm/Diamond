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
		string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "download.json");
		string jsonFileContents = File.ReadAllText(path);

		string itemsEurResponse = RequestUtils.Get("http://csgobackpack.net/api/GetItemsList/v2/?currency=eur");
		CsgoItemsList? itemsEur = JsonConvert.DeserializeObject<CsgoItemsList?>(jsonFileContents);
		string itemsUsdResponse = RequestUtils.Get("https://csgobackpack.net/api/GetItemsList/v2/?currency=usd&no_details=true");
		CsgoItemsList? itemsUsd = JsonConvert.DeserializeObject<CsgoItemsList?>(itemsEurResponse);
		string itemsBrlResponse = RequestUtils.Get("https://csgobackpack.net/api/GetItemsList/v2/?currency=brl&no_details=true");
		CsgoItemsList? itemsBrl = JsonConvert.DeserializeObject<CsgoItemsList?>(itemsBrlResponse);

		if (itemsEur != null)
		{
			using (CsgoDatabase? database = new CsgoDatabase())
			{
				database.Database.ExecuteSqlRaw("DELETE FROM Prices");
				database.Database.ExecuteSqlRaw("DELETE FROM Items");
				database.Database.ExecuteSqlRaw("DELETE FROM Rarities");

				List<string> addedIds = new List<string>();
				List<CsgoRarity> addedRarities = new List<CsgoRarity>();
				foreach (CsgoItemInfo item in itemsEur.ItemsList.Values)
				{
					if (addedIds.Contains(item.ClassID))
					{
						continue;
					}
					addedIds.Add(item.ClassID);

					CsgoItem addItem = new CsgoItem
					{
						Id = Convert.ToInt64(item.ClassID),
						Name = item.Name.Replace("&#39", "\""),
						IconUrl = item.IconUrl,
						Type = item.Type,
					};
					if (!item.Rarity.IsEmpty())
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
						addItem.Rarity = rarity;
					}
					database.Add(addItem);

					if (item.Price != null)
					{
						List<string> times = new List<string>()
						{
							"24_hours",
							"7_days",
							"30_days",
							"all_time",
						};

						foreach (string time in times)
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
				}
				database.SaveChanges();
			}
		}
	}
}