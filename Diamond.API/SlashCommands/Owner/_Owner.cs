using Discord.Interactions;

namespace Diamond.API.SlashCommands.Owner
{
	[RequireOwner]
	[Group("owner", ":)")]
	public partial class Owner : InteractionModuleBase<SocketInteractionContext> { }
}