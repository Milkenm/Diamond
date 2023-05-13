using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.APIs;
using Diamond.API.Schemes.SteamInventory;

using Discord;
using Discord.Interactions;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.CSGO;


public partial class Csgo
{
	// <SteamID, EvaluateCache>
	public static Dictionary<string, EvaluateCache> _evaluateCache = new Dictionary<string, EvaluateCache>();

	private const int CACHE_KEEP_SECONDS = 15 * 60; // 15 minutes
	private const string ID_URL = "https://steamcommunity.com/inventory/{0}/730/2?l=english&count=5000";
	private const string VANITY_URL = "https://steamcommunity.com/id/{0}";

	[SlashCommand("evaluate", "Estimates the value of a CS:GO inventory.")]
	public async Task CsgoEvaluateCommandAsync(
		[Summary("search", "The user's username/steamid to evaluate.")] string user,
		[Summary("currency", "The currency to return the price in.")] Currency currency = Currency.EUR,
		[Summary("force-refresh", "Force the refresh of the inventory.")] bool forceRefresh = false,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		// Create embed
		DefaultEmbed embed = new DefaultEmbed("CS:GO Inventory value", "💸", Context.Interaction);

		// Vars
		bool fromCache = false;

		SteamUserInfo userInfo = null;
		SteamInventory inventory = null;
		double totalValue = 0D;
		int uniqueItems = 0;
		int totalItems = 0;
		int ignoredItems = 0;
		string mostValuableItemName = null;
		double mostValuableItemValue = 0D;

		// Get user and inventory info from cache
		EvaluateCache cachedUser = null;
		if (_evaluateCache.Count > 0)
		{
			cachedUser = _evaluateCache.Values.Where(ec => ec.Searches.Contains(user) && ec.Currency == currency && (ec.RefreshedAt + CACHE_KEEP_SECONDS) >= DateTimeOffset.UtcNow.ToUnixTimeSeconds()).First();
		}
		if (cachedUser != null)
		{
			fromCache = true;

			userInfo = cachedUser.UserInfo;
			totalValue = cachedUser.InventoryValue;
			uniqueItems = cachedUser.UniqueItems;
			totalItems = cachedUser.TotalItems;
			ignoredItems = cachedUser.IgnoredItems;
			mostValuableItemName = cachedUser.MostValuableItemName;
			mostValuableItemValue = cachedUser.MostValuableItemValue;
		}
		// Get user and inventory from Steam
		else
		{
			// Get Steam profile
			string steamProfileUrl = "";
			if (long.TryParse(user, out long steamId))
			{
				steamProfileUrl = string.Format(ID_URL, steamId);
				userInfo = GetUserInfo(RequestUtils.Get(steamProfileUrl));
			}
			if (userInfo == null)
			{
				steamProfileUrl = string.Format(VANITY_URL, user);
				userInfo = GetUserInfo(RequestUtils.Get(steamProfileUrl));
			}
			if (userInfo == null)
			{
				embed.WithDescription("**The profile you searched for could not be found.**");

				await embed.SendAsync();
				return;
			}
			userInfo.SteamProfileUrl = steamProfileUrl;

			// Get inventory items
			try
			{
				string inventoryResponse = RequestUtils.Get(string.Format(ID_URL, userInfo.SteamID));
				inventory = JsonConvert.DeserializeObject<SteamInventory>(inventoryResponse);
			}
			catch
			{
				embed.WithDescription($"**There was an error downloading the inventory.**");

				await embed.SendAsync();
				return;
			}

			// Organize items in a dictionary
			Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
			foreach (Asset asset in inventory.Assets)
			{
				if (keyValuePairs.ContainsKey(asset.ClassID))
				{
					keyValuePairs[asset.ClassID]++;
				}
				else
				{
					keyValuePairs.Add(asset.ClassID, 1);
				}
			}

			// Calculate inventory value
			foreach (AssetDescription description in inventory.Descriptions)
			{
				CsgoItemMatchInfo csgoItem = _csgoBackpack.SearchItems(description.MarketName, currency)[0];
				if (csgoItem.CsgoItem.Price == null)
				{
					ignoredItems++;
					continue;
				}
				double itemPrice = csgoItem.CsgoItem.Price.Values.First().Average * keyValuePairs[description.ClassID];
				if (itemPrice > mostValuableItemValue)
				{
					mostValuableItemName = csgoItem.CsgoItem.Name;
					mostValuableItemValue = itemPrice;
				}
				totalValue += itemPrice;
			}

			// Unique/Total items
			uniqueItems = inventory.Descriptions.Count;
			totalItems = inventory.Assets.Count;
		}

		if (!fromCache)
		{
			// Cache results
			_evaluateCache.Remove(userInfo.SteamID);

			EvaluateCache userCache = new EvaluateCache()
			{
				Searches = new List<string>() { user },
				Currency = currency,
				UserInfo = userInfo,
				Inventory = inventory,
				InventoryValue = totalValue,
				UniqueItems = uniqueItems,
				TotalItems = totalItems,
				IgnoredItems = ignoredItems,
				MostValuableItemName = mostValuableItemName,
				MostValuableItemValue = mostValuableItemValue
			};
			_evaluateCache.Add(userInfo.SteamID, userCache);
		}

		// Send embed
		embed.WithThumbnailUrl(userInfo.AvatarUrl);
		embed.WithTitle(userInfo.Username);
		embed.AddField("Value", $"{string.Format("{0:0.00}", totalValue)}{CsgoBackpack.CurrencySymbols[currency]}");
		embed.AddField("Most valuable item", $"{mostValuableItemName} ({string.Format("{0:0.00}", mostValuableItemValue)}{CsgoBackpack.CurrencySymbols[currency]})");
		embed.AddField("Unique items", uniqueItems, true);
		embed.AddField("Total items", totalItems, true);
		embed.AddField("Unpriced items", ignoredItems, true);

		ComponentBuilder component = new ComponentBuilder()
			.WithButton(new ButtonBuilder("View Steam Profile", style: ButtonStyle.Link, url: userInfo.SteamProfileUrl, emote: Emoji.Parse("🎮")))
			.WithButton(new ButtonBuilder("View on CS:GO Backpack", style: ButtonStyle.Link, url: $"https://csgobackpack.net/index.php?nick={userInfo.SteamID}", emote: Emoji.Parse("💰")));

		await embed.SendAsync(component.Build());
	}

	public class EvaluateCache
	{
		public EvaluateCache()
		{
			RefreshedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}

		public List<string> Searches { get; set; }
		public Currency Currency { get; set; }
		public SteamUserInfo UserInfo { get; set; }
		public SteamInventory Inventory { get; set; }
		public double InventoryValue { get; set; }
		public int UniqueItems { get; set; }
		public int TotalItems { get; set; }
		public int IgnoredItems { get; set; }
		public string MostValuableItemName { get; set; }
		public double MostValuableItemValue { get; set; }
		public long RefreshedAt { get; }
	}

	public class SteamUserInfo
	{
		[JsonProperty("url")] public string Url { get; set; }
		[JsonProperty("steamid")] public string SteamID { get; set; }
		[JsonProperty("personaname")] public string Username { get; set; }
		[JsonProperty("summary")] public string AboutMe { get; set; }
		[JsonIgnore] public string AvatarUrl { get; set; }
		[JsonIgnore] public string SteamProfileUrl { get; set; }
	}

	private static SteamUserInfo? GetUserInfo(string pageContent)
	{
		SteamUserInfo userInfo = null;
		string avatarUrl = "";
		foreach (string line in pageContent.Split("\n"))
		{
			string trimmedLine = line.Trim();
			if (trimmedLine.StartsWith("g_rgProfileData = {"))
			{
				string userJson = trimmedLine.Remove(trimmedLine.Length - 1).Replace("g_rgProfileData = ", "");
				userInfo = JsonConvert.DeserializeObject<SteamUserInfo>(userJson);
			}
			else if (trimmedLine.StartsWith("<meta name=\"twitter:image\" content=\"https://avatars.akamai.steamstatic.com/"))
			{
				avatarUrl = trimmedLine.Replace(" ", "").Replace("<metaname=\"twitter:image\"content=\"", "").Replace("\"/>", "");
			}
		}
		if (userInfo != null)
		{
			userInfo.AvatarUrl = avatarUrl;
		}
		return userInfo;
	}
}
