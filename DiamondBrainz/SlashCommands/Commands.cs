using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands;
public class Commands : InteractionModuleBase<SocketInteractionContext>
{
	private readonly InteractionService _interactionService;

	public Commands(InteractionService interactionService)
	{
		this._interactionService = interactionService;
	}

	[DSlashCommand("commands", "View all commands the bot has.")]
	public async Task CommandsCommandAsync(
		[ShowEveryone] bool showEveryone = false
	)
	{
		await this.DeferAsync(!showEveryone);

		StringBuilder sb = new StringBuilder();
		foreach (SlashCommandInfo slashCommand in this._interactionService.SlashCommands.OrderBy(s => s.Name).Take(new Range(0, 6)))
		{
			if (sb.Length > 0)
			{
				sb.Append("\n\n");
			}
			sb.Append($"🔷 **__{slashCommand.Name}__**\n🔹 {slashCommand.Description}");
		}

		DefaultEmbed embed = new DefaultEmbed("Commands", "🤖", this.Context)
		{
			Description = sb.ToString(),
		};
		await embed.SendAsync();
	}
}
