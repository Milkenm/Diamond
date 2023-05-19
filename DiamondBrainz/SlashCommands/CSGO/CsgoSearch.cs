using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Diamond.API.APIs;
using Diamond.API.Data;

using Discord;
using Discord.Interactions;

using ScriptsLibV2.Extensions;

using static Diamond.API.Utils;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API.SlashCommands.CSGO;

public partial class Csgo
{
	private static readonly Dictionary<string, Bitmap> _raritiesCacheMap = new Dictionary<string, Bitmap>();

	[SlashCommand("item", "Search for a CS:GO item.")]
	public async Task CsgoSearchCommandAsync(
		[Summary("search", "The name of the item to search for."), Autocomplete(typeof(ExampleAutocompleteHandler))] string search,
		[Summary("currency", "The currency to return the price in.")] Currency currency = Currency.EUR,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await this.DeferAsync(!showEveryone);

		DefaultEmbed embed = new DefaultEmbed("CS:GO Item Search", "🔫", this.Context.Interaction);

		// Clear cache if debug is enabled
		if (SUtils.IsDebugEnabled())
		{
			this._csgoBackpack.ClearCache();
		}

		// Get best maching item
		List<SearchMatchInfo<DbCsgoItem>> searchResult = await this._csgoBackpack.SearchItemAsync(search);
		if (searchResult.Count == 0)
		{
			embed.Title = "Item not found";
			embed.Description = $"An item named '{search}' could not be found.";
			await embed.SendAsync();
			return;
		}
		DbCsgoItem searchItem = searchResult[0].Item;
		List<DbCsgoItemPrice> searchItemPrices = _csgoBackpack.GetItemPrices(searchItem, currency);

		embed.Title = searchItem.Name;
		embed.Description = $"⭐ **Released**: {UnixTimeStampToDateTime(searchItem.FirstSaleDateUnix).AddDays(1).ToString("dd/MM/yyyy")}";
		embed.ThumbnailUrl = $"https://community.cloudflare.steamstatic.com/economy/image/{searchItem.IconUrl}";
		ComponentBuilder builder = new ComponentBuilder()
			.WithButton(new ButtonBuilder("View on Steam market", style: ButtonStyle.Link, url: $"https://steamcommunity.com/market/listings/730/{searchItem.Name}".Replace(" ", "%20").Replace("|", "%7C"), emote: Emoji.Parse("🏪")))
			.WithButton(new ButtonBuilder("Buy on DMarket", style: ButtonStyle.Link, url: $"https://dmarket.com/ingame-items/item-list/csgo-skins?title={HttpUtility.UrlEncode(searchItem.Name)}&ref=3Ge3jlBrCg", emote: Emoji.Parse("🛒")));

		foreach (DbCsgoItemPrice priceInfo in searchItemPrices)
		{
			embed.AddField($"🗓️ {Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(priceInfo.Epoch.ToLower().Replace("_", " "))}", $"**{CsgoBackpack.CurrencySymbols[currency]}{string.Format("{0:N}", priceInfo.Average)}**\n*{string.Format("{0:N0}", Convert.ToInt32(priceInfo.Sold))} sold*", true);
		}

		string hexColor = searchItem.RarityHexColor;

		Bitmap bmp;
		if (_raritiesCacheMap.ContainsKey(hexColor))
		{
			bmp = _raritiesCacheMap[hexColor];
		}
		else
		{
			bmp = DrawLine(hexColor);
			_raritiesCacheMap.Add(hexColor, bmp);
		}

		using (Stream stream = new MemoryStream())
		{
			embed.WithImageUrl($"attachment://csgo_{hexColor}.png");
			bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

			// Send the embed
			await base.FollowupWithFileAsync(stream, $"csgo_{hexColor}.png", embed: embed.Build(), components: builder.Build());
		}
	}

	/*[AutocompleteCommand("search", "item")]
	public async Task Autocomplete()
	{
		SocketAutocompleteInteraction interaction = this.Context.Interaction as SocketAutocompleteInteraction;

		string userInput = interaction.Data.Current.Value.ToString();
		if (userInput.IsEmpty())
		{
			await interaction.RespondAsync(null);
			return;
		}
		// Get best maching item
		List<SearchMatchInfo<DbCsgoItem>> searchResult = await this._csgoBackpack.SearchItemAsync(userInput);
		if (searchResult.Count == 0)
		{
			await interaction.RespondAsync(null);
			return;
		}

		List<AutocompleteResult> autocomplete = new List<AutocompleteResult>();
		foreach (SearchMatchInfo<DbCsgoItem> result in searchResult)
		{
			string itemName = result.Item.Name;
			autocomplete.Add(new AutocompleteResult(itemName, itemName));
		}

		try
		{
			await interaction.RespondAsync(autocomplete.Take(25));
		}
		catch { }
	}*/

	public class ExampleAutocompleteHandler : AutocompleteHandler
	{
		private readonly CsgoBackpack _csgoBackpack;

		public ExampleAutocompleteHandler(CsgoBackpack csgoBackpack)
		{
			_csgoBackpack = csgoBackpack;
		}

		public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			string userInput = autocompleteInteraction.Data.Current.Value.ToString();
			if (userInput.IsEmpty())
			{
				return AutocompletionResult.FromSuccess(null);
			}
			// Get best maching item
			List<SearchMatchInfo<DbCsgoItem>> searchResult = await this._csgoBackpack.SearchItemAsync(userInput);
			if (searchResult.Count == 0)
			{
				return AutocompletionResult.FromSuccess(null);
			}

			List<AutocompleteResult> autocomplete = new List<AutocompleteResult>();
			foreach (SearchMatchInfo<DbCsgoItem> result in searchResult)
			{
				string itemName = result.Item.Name;
				autocomplete.Add(new AutocompleteResult(itemName, itemName));
			}

			IEnumerable<AutocompleteResult> sendResults = autocomplete.Take(10);

			sw.Stop();
			Debug.WriteLine($"Sending autocomplete (took: {sw.ElapsedMilliseconds}ms, results: {autocomplete.Count}).");
			return AutocompletionResult.FromSuccess(sendResults);
		}
	}

	public static Bitmap DrawLine(string hexColor)
	{
		Bitmap bitmap = new Bitmap(500, 5);

		for (int x = 0; x < bitmap.Width; x++)
		{
			for (int y = 0; y < bitmap.Height; y++)
			{
				bitmap.SetPixel(x, y, ColorTranslator.FromHtml($"#{hexColor}"));
			}
		}

		return bitmap;
	}
}
