using Discord.Interactions;

namespace Diamond.API.SlashCommands.NSFW
{
	[RequireNsfw]
	[NsfwCommand(true)]
	[Group("nsfw", "Sus commands.")]
	public partial class NSFW : InteractionModuleBase<SocketInteractionContext> { }
}