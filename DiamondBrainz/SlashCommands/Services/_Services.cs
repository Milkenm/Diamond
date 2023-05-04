using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Services;
[Group("service", "Service commands, like APIs and webhooks.")]
public partial class Services : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiscordSocketClient _client;

	public Services(DiscordSocketClient client)
	{
		_client = client;
	}
}
