using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.SteamInventory
{
	public class SteamInventory
	{
		[JsonProperty("assets")] public List<Asset> Assets { get; set; }
		[JsonProperty("descriptions")] public List<AssetDescription> Descriptions { get; set; }
	}

	public class Asset
	{
		[JsonProperty("appid")] public int AppID { get; set; }
		[JsonProperty("contextid")] public string ContextID { get; set; }
		[JsonProperty("assetid")] public string AssetID { get; set; }
		[JsonProperty("classid")] public string ClassID { get; set; }
		[JsonProperty("instanceid")] public string InstanceID { get; set; }
		[JsonProperty("amount")] public string Amount { get; set; }
	}

	public class AssetDescription
	{
		[JsonProperty("appid")] public int AppID { get; set; }
		[JsonProperty("classid")] public string ClassID { get; set; }
		[JsonProperty("instanceid")] public string InstanceID { get; set; }
		[JsonProperty("currency")] public int Currency { get; set; }
		[JsonProperty("background_color")] public string BackgroundColor { get; set; }
		[JsonProperty("icon_url")] public string IconUrl { get; set; }
		[JsonProperty("descriptions")] public List<Description> Descriptions { get; set; }
		[JsonProperty("tradeable")] public bool Tradeable { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("name_color")] public string NameColor { get; set; }
		[JsonProperty("type")] public string Type { get; set; }
		[JsonProperty("market_name")] public string MarketName { get; set; }
		[JsonProperty("market_hash_name")] public string MarketHashName { get; set; }
		[JsonProperty("marketable")] public bool Marketable { get; set; }
		[JsonProperty("tags")] public List<Tag> Tags { get; set; }
		[JsonProperty("market_buy_country_restriction")] public string MarketBuyCountryRestriction { get; set; }
	}

	public class Description
	{
		[JsonProperty("type")] public string Type { get; set; }
		[JsonProperty("value")] public string Value { get; set; }
		[JsonProperty("color")] public string Color { get; set; }
	}

	public class Tag
	{
		[JsonProperty("category")] public string Category { get; set; }
		[JsonProperty("internal_name")] public string InternalName { get; set; }
		[JsonProperty("localized_category_name")] public string LocalizedCategoryName { get; set; }
		[JsonProperty("localized_tag_name")] public string LocalizedTagName { get; set; }
		[JsonProperty("color")] public string Color { get; set; }
	}
}
