using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Schems;
using Diamond.API.Stuff;

using Discord.Interactions;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.CSGO;

[Group("csgo", "CS:GO related commands.")]
public partial class CsgoSearch : InteractionModuleBase<SocketInteractionContext>
{
	private const string ID_URL = "https://steamcommunity.com/inventory/{0}/730/2?l=english&count=5000";
	private const string VANITY_URL = "https://steamcommunity.com/id/{0}";

	[SlashCommand("evaluate", "Estimates the value of a CS:GO inventory.")]
	public async Task CsgoEvaluateCommandAsync(
		[Summary("search", "The user's username/steamid to evaluate.")] string user,
		[Summary("currency", "The currency to return the price in.")] Currency currency = Currency.EUR,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		// Get Steam profile
		SteamUserInfo userInfo = null;
		if (long.TryParse(user, out long steamId))
		{
			userInfo = GetUserInfo(RequestUtils.Get(string.Format(ID_URL, steamId)));
		}
		if (userInfo == null)
		{
			userInfo = GetUserInfo(RequestUtils.Get(string.Format(VANITY_URL, user)));
		}

		if (userInfo == null)
		{
			await FollowupAsync("Profile not found.");
			return;
		}

		// Get inventory items
		CsgoInventory inventory;
		try
		{
			string inventoryResponse = RequestUtils.Get(string.Format(ID_URL, userInfo.SteamID));
			inventory = JsonConvert.DeserializeObject<CsgoInventory>(inventoryResponse);
		}
		catch
		{
			await FollowupAsync("Error downloading inventory.");
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
		double totalValue = 0D;
		foreach (CsgoInventoryItemEntryDescription description in inventory.Descriptions)
		{
			CsgoItemMatchInfo csgoItem = _csgoBackpack.SearchItem(description.MarketName, Currency.EUR);
			if (csgoItem.CsgoItem.Price != null)
			{
				double itemPrice = csgoItem.CsgoItem.Price.Values.First().Average * keyValuePairs[description.ClassID];
				totalValue += itemPrice;
			}
		}

		// Send embed
		DefaultEmbed embed = new DefaultEmbed("Inventory value", "💸", Context.Interaction);
		embed.WithDescription($"User: {userInfo.Username}");
		embed.AddField("Value", $"{Math.Round(totalValue, 2)}€");
		embed.AddField("Unique items", inventory.Descriptions.Count, true);
		embed.AddField("Total items", inventory.Assets.Count, true);

		await embed.SendAsync();
	}

	public class SteamUserInfo
	{
		[JsonProperty("url")] public string Url { get; set; }
		[JsonProperty("steamid")] public string SteamID { get; set; }
		[JsonProperty("personaname")] public string Username { get; set; }
		[JsonProperty("summary")] public string AboutMe { get; set; }
	}

	private static SteamUserInfo? GetUserInfo(string pageContent)
	{
		SteamUserInfo userInfo = null;
		foreach (string line in pageContent.Split("\n"))
		{
			string trimmedLine = line.Trim();
			if (trimmedLine.StartsWith("g_rgProfileData = {"))
			{
				string userJson = trimmedLine.Remove(trimmedLine.Length - 1).Replace("g_rgProfileData = ", "");
				userInfo = JsonConvert.DeserializeObject<SteamUserInfo>(userJson);
			}
		}
		return userInfo;
	}
}
