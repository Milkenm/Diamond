using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Services
{
	[Group("service", "Service commands, like APIs and webhooks.")]
	public partial class Services : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly DiamondClient _client;

		public Services(DiamondClient client)
		{
			this._client = client;
		}
	}
}