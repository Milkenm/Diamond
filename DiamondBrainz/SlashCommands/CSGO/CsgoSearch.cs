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
using Diamond.API.Attributes;
using Diamond.API.Data;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API.SlashCommands.CSGO
{
	public partial class Csgo
	{
		private static readonly Dictionary<string, Bitmap> _raritiesCacheMap = new Dictionary<string, Bitmap>();

		[DSlashCommand("item", "Search for a CS:GO item.")]
		public async Task CsgoSearchCommandAsync(
			[Summary("search", "The name of the item to search for."), Autocomplete] string search,
			[Summary("currency", "The currency to return the price in.")] Currency currency = Currency.EUR,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			DefaultEmbed embed = new DefaultEmbed("CS:GO Item Search", "🔫", this.Context);

			if (SUtils.IsDebugEnabled())
			{
				this._csgoBackpack.ClearCache();
			}

			// Lock thread while items are not loaded
			while (!this._csgoBackpack.AreItemsLoaded)
			{
				await Task.Delay(1000);
			}

			// Get best maching item
			List<SearchMatchInfo<DbCsgoItem>> searchResult = this._csgoBackpack.SearchItem(search);
			if (searchResult.Count == 0)
			{
				embed.Title = "Item not found";
				embed.Description = $"An item named '{search}' could not be found.";
				_ = await embed.SendAsync();
				return;
			}
			DbCsgoItem searchItem = searchResult[0].Item;
			List<DbCsgoItemPrice> searchItemPrices = this._csgoBackpack.GetItemPrices(searchItem, currency);

			embed.Title = searchItem.Name;
			embed.Description = $"**Introduced**: {Utils.GetTimestampBlock(searchItem.FirstSaleDateUnix, TimestampSetting.TimeSince)}";
			embed.ThumbnailUrl = $"https://community.cloudflare.steamstatic.com/economy/image/{searchItem.IconUrl}";
			ComponentBuilder builder = new ComponentBuilder()
				.WithButton(new ButtonBuilder("View on Steam market", style: ButtonStyle.Link, url: $"https://steamcommunity.com/market/listings/730/{searchItem.Name}".Replace(" ", "%20").Replace("|", "%7C"), emote: Emoji.Parse("🏪")))
				.WithButton(new ButtonBuilder("Buy on DMarket", style: ButtonStyle.Link, url: $"https://dmarket.com/ingame-items/item-list/csgo-skins?title={HttpUtility.UrlEncode(searchItem.Name)}&ref=3Ge3jlBrCg", emote: Emoji.Parse("🛒")));

			foreach (DbCsgoItemPrice priceInfo in searchItemPrices)
			{
				_ = embed.AddField($"🗓️ {Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(priceInfo.Epoch.ToLower().Replace("_", " "))}", $"**{CsgoBackpack.CurrencySymbols[currency]}{string.Format("{0:N}", priceInfo.Average)}**\n*{string.Format("{0:N0}", Convert.ToInt32(priceInfo.Sold))} sold*", true);
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
				_ = embed.WithImageUrl($"attachment://csgo_{hexColor}.png");
				bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

				// Send the embed
				_ = await base.FollowupWithFileAsync(stream, $"csgo_{hexColor}.png", embed: embed.Build(), components: builder.Build());
			}
		}

		[AutocompleteCommand("search", "item")]
		public async Task Autocomplete()
		{
			SocketAutocompleteInteraction autocompleteInteraction = this.Context.Interaction as SocketAutocompleteInteraction;

			// Ignore autocomplete if items are not loaded
			if (!this._csgoBackpack.AreItemsLoaded)
			{
				await autocompleteInteraction.RespondAsync(null);
				return;
			}

			Stopwatch sw = null;
			if (SUtils.IsDebugEnabled())
			{
				sw = new Stopwatch();
				sw.Start();
			}

			string userInput = autocompleteInteraction.Data.Current.Value.ToString();
			if (userInput.IsEmpty())
			{
				await autocompleteInteraction.RespondAsync(null);
				return;
			}
			// Get best maching item
			List<SearchMatchInfo<DbCsgoItem>> searchResult = this._csgoBackpack.SearchItem(userInput);
			if (searchResult.Count == 0)
			{
				await autocompleteInteraction.RespondAsync(null);
				return;
			}

			List<AutocompleteResult> autocomplete = new List<AutocompleteResult>();
			foreach (SearchMatchInfo<DbCsgoItem> result in searchResult)
			{
				string itemName = result.Item.Name;
				autocomplete.Add(new AutocompleteResult(itemName, itemName));
			}
			IEnumerable<AutocompleteResult> sendResults = autocomplete.Take(5);

			if (SUtils.IsDebugEnabled())
			{
				sw.Stop();
				Debug.WriteLine($"Sending autocomplete (took: {sw.ElapsedMilliseconds}ms, results: {autocomplete.Count}).");
			}

			await autocompleteInteraction.RespondAsync(sendResults);
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
}