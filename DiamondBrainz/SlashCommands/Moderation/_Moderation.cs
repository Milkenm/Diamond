using Discord.Interactions;

namespace Diamond.API.SlashCommands.Moderation
{
	[Group("mod", "Moderation related commands.")]
	public partial class Moderation : InteractionModuleBase<SocketInteractionContext> { }
}