using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Services
{
	[EnabledInDm(false)]
	[RequireUserPermission(GuildPermission.Administrator)]
	[DefaultMemberPermissions(GuildPermission.Administrator)]
	[Group("service", "Service commands, like APIs and webhooks.")]
	public partial class Services : InteractionModuleBase<SocketInteractionContext> { }
}