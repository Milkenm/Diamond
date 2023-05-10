using System.Text;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands;
public class Commands : InteractionModuleBase<SocketInteractionContext>
{
	private readonly InteractionService _interactionService;

	public Commands(InteractionService interactionService)
	{
		this._interactionService = interactionService;
	}

	[SlashCommand("commands", "View all commands the bot has.")]
	public async Task CommandsCommandAsync(
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		StringBuilder sb = new StringBuilder();
		foreach (SlashCommandInfo slashCommand in _interactionService.SlashCommands)
		{
			if (sb.Length > 0)
			{
				sb.Append("\n");
			}
			sb.Append($"🔷 {slashCommand.Name}\n🔹 {slashCommand.Description}");
		}

		DefaultEmbed embed = new DefaultEmbed("Commands", "🤖", Context.Interaction)
		{
			Description = sb.ToString(),
		};
		await embed.SendAsync();
	}
}
