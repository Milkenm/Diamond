using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands;
public class CreateEmbed : InteractionModuleBase<SocketInteractionContext>
{
	[SlashCommand("create-button", "Create a nice button with a link.")]
	public async Task CreateEmbedCommandAsync(
		[Summary("text", "The text to show on the button.")] string text,
		[Summary("link", "The link to go to when the button is clicked.")] string link
		)
	{
		await DeferAsync();

		ComponentBuilder builder = new ComponentBuilder()
			.WithButton(new ButtonBuilder(text, style: ButtonStyle.Link, url: link));

		await FollowupAsync(" ", components: builder.Build());
	}
}
