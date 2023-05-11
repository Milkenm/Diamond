using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Diamond.API.APIs;
using Diamond.API.Schems.CsgoBackpack;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.CSGO;

public partial class Csgo
{
	private static readonly Dictionary<string, Bitmap> _raritiesCacheMap = new Dictionary<string, Bitmap>();

	[SlashCommand("item", "Search for a CS:GO item.")]
	public async Task CsgoSearchCommandAsync(
		[Summary("search", "The name of the item to search for."), Autocomplete] string search,
		[Summary("currency", "The currency to return the price in.")] Currency currency = Currency.EUR,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await this.DeferAsync(!showEveryone);

		this._csgoBackpack.ClearCache();
		CsgoItemMatchInfo resultItem = this._csgoBackpack.SearchItems(search, currency).First();

		DefaultEmbed embed = new DefaultEmbed("CS:GO Item Search", "🔫", this.Context.Interaction)
		{
			Title = resultItem.CsgoItem.Name,
			Description = $"⭐ **Released**: {UnixTimeStampToDateTime(resultItem.CsgoItem.FirstSaleDate).AddDays(1).ToString("dd/MM/yyyy")}",
			ThumbnailUrl = $"https://community.cloudflare.steamstatic.com/economy/image/{resultItem.CsgoItem.IconUrl}",
		};
		ComponentBuilder builder = new ComponentBuilder()
			.WithButton(new ButtonBuilder("View on Steam market", style: ButtonStyle.Link, url: $"https://steamcommunity.com/market/listings/730/{resultItem.CsgoItem.Name}".Replace(" ", "%20").Replace("|", "%7C"), emote: Emoji.Parse("🏪")))
			.WithButton(new ButtonBuilder("Buy on DMarket", style: ButtonStyle.Link, url: $"https://dmarket.com/ingame-items/item-list/csgo-skins?title={HttpUtility.UrlEncode(resultItem.CsgoItem.Name)}&ref=3Ge3jlBrCg", emote: Emoji.Parse("🛒")));

		foreach (KeyValuePair<string, CsgoItemPriceInfo> priceKeyPair in resultItem.CsgoItem.Price)
		{
			embed.AddField($"🗓️ {Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(priceKeyPair.Key.ToString().ToLower().Replace("_", " "))}", $"**{CsgoBackpack.CurrencySymbols[currency]}{string.Format("{0:N}", priceKeyPair.Value.Average)}**\n*{string.Format("{0:N0}", Convert.ToInt32(priceKeyPair.Value.Sold))} sold*", true);
		}

		string hexColor = resultItem.CsgoItem.RarityHexColor;

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

	[AutocompleteCommand("search", "item")]
	public async Task Autocomplete()
	{
		SocketAutocompleteInteraction interaction = this.Context.Interaction as SocketAutocompleteInteraction;

		string userInput = interaction.Data.Current.Value.ToString();
		if (userInput.IsEmpty())
		{
			await interaction.RespondAsync(null);
			return;
		}

		List<CsgoItemMatchInfo> matches = this._csgoBackpack.SearchItems(userInput, Currency.EUR);

		List<AutocompleteResult> autocomplete = new List<AutocompleteResult>();
		foreach (CsgoItemMatchInfo match in matches)
		{
			string itemName = match.CsgoItem.Name;
			autocomplete.Add(new AutocompleteResult(itemName, itemName));
		}

		await interaction.RespondAsync(autocomplete.Take(25));
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

	public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
	{
		DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
		return dtDateTime;
	}
}
