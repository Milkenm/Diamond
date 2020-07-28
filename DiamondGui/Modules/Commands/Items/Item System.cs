using Discord;
using Discord.Commands;

using System.Threading.Tasks;

using static DiamondGui.Functions;

namespace DiamondGui.Commands
{
	public class ItemSystem : ModuleBase<SocketCommandContext>
	{
		[Command("shop"), Alias("shop"), Summary("Shop - Menu")]
		public async Task CMD_Shop(string options)
		{
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor(AuthorBuilder("Shop", "", ""));

			switch (options.ToLower())
			{
				case "buy":
					embed.WithDescription("Buy Items: shop buy <item> <amount>");
					break;

				case "sell":
					embed.WithDescription("Sell Items: shop sell <item> <amount>");
					break;

				default:
					embed.WithDescription("Use: shop buy <item> <amount> or shop sell <item>");
					break;
			}

			await Context.Channel.SendMessageAsync("", false, FinishEmbed(embed, Context)).ConfigureAwait(false);
		}

		[Command("shop"), Alias("s"), Summary("Shop")]
		public async Task CMD_Items_Loja(string action, string item = null, int amount = 0)
		{
			if (action == "list")
			{
				string itemsList = string.Empty;
				foreach (string iinfo in new string[] { })
				{
					if (!string.IsNullOrEmpty(itemsList))
					{
						itemsList += $",\n{FormatItemListingMessage(iinfo, 0)}";
					}
					else
					{
						itemsList = FormatItemListingMessage(iinfo, 0);
					}
				}
				await ReplyAsync($"__Available Items:__\n{itemsList}.").ConfigureAwait(false);
			}
			else
			{
				string iinfo = null;

				if (iinfo == null)
				{
					await ReplyAsync("That item was not found!").ConfigureAwait(false);
				}
				else if (amount <= 0)
				{
					await ReplyAsync("Invalid amount!").ConfigureAwait(false);
				}
				else
				{
					switch (action)
					{
						case "buy":
						case "b":
						{
							await ReplyAsync($"You purchased {FormatItemMessage(iinfo, 0, amount)} :euro:.").ConfigureAwait(false);
							break;
						}
						case "sell":
						case "s":
						{
							await ReplyAsync($"You sold {FormatItemMessage(iinfo, 0, amount)} :euro:.").ConfigureAwait(false);
							break;
						}
					}
				}
			}
		}
	}
}