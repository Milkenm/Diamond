using Discord.Interactions;

namespace Diamond.API.SlashCommands.Moderation
{
	[EnabledInDm(false)]
	[Group("mod", "Moderation related commands.")]
	public partial class Moderation : InteractionModuleBase<SocketInteractionContext> { }
}