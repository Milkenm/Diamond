using Discord.Interactions;

namespace Diamond.API.SlashCommands.NSFW
{
	[RequireNsfw]
	[Group("nsfw", "Sus commands.")]
	public partial class NSFW : InteractionModuleBase<SocketInteractionContext> { }
}