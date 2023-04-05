using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schems
{
	public class CsgoItemsList
	{
		[JsonProperty("success")] public bool Success { get; set; }
		[JsonProperty("currency")] public string Currency { get; set; }
		[JsonProperty("timestamp")] public long Timestamp { get; set; }
		[JsonProperty("items_list")] public Dictionary<string, CsgoItemInfo> ItemsList { get; set; }
	}

	public class CsgoItemInfo
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("marketable")] public int Marketable { get; set; }
		[JsonProperty("tradeable")] public int Tradeable { get; set; }
		[JsonProperty("classid")] public string ClassID { get; set; }
		[JsonProperty("icon_url")] public string IconUrl { get; set; }
		[JsonProperty("icon_url_large")] public string LargeIconUrl { get; set; }
		[JsonProperty("type")] public string Type { get; set; }
		[JsonProperty("rarity")] public string Rarity { get; set; }
		[JsonProperty("rarity_color")] public string RarityHexColor { get; set; }
		[JsonProperty("price")] public Dictionary<string, CsgoItemPriceInfo> Price { get; set; }
		[JsonProperty("first_sale_date")] public long FirstSaleDate { get; set; }
	}

	public class CsgoItemPriceInfo
	{
		[JsonProperty("average")] public double Average { get; set; }
		[JsonProperty("median")] public double Median { get; set; }
		[JsonProperty("sold")] public string Sold { get; set; }
		[JsonProperty("standard_deviation")] public string StandardDeviation { get; set; }
		[JsonProperty("lowest_price")] public double LowestPrice { get; set; }
		[JsonProperty("highest_price")] public double HighestPrice { get; set; }
	}
}
