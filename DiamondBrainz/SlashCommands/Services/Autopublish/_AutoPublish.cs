using Discord.Interactions;

namespace Diamond.API.SlashCommands.Services
{
	public partial class Services
	{
		[Group("auto-publish", "Automatically publishes messages sent to announcement channels.")]
		public partial class AutoPublish : InteractionModuleBase<SocketInteractionContext>
		{
			private readonly DiamondClient _client;

			public AutoPublish(DiamondClient client)
			{
				this._client = client;

				this._client.MessageReceived += this.OnClientMessageReceived;
			}
		}
	}
}