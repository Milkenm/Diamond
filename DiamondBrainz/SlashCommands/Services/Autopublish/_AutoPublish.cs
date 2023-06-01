using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Services
{
	public partial class Services
	{
		[Group("auto-publish", "Automatically publishes messages sent to announcement channels.")]
		public partial class AutoPublish : InteractionModuleBase<SocketInteractionContext>
		{
			private readonly DiamondClient _client;

			private static bool _eventInitialized = false;

			public AutoPublish(DiamondClient client)
			{
				this._client = client;

				// Initialize events
				if (!_eventInitialized)
				{
					_eventInitialized = true;

					this._client.MessageReceived += this.OnClientMessageReceived;
				}
			}

			public DefaultEmbed DefaultEmbed => new DefaultEmbed("Auto Publisher", "📣", this.Context);

			private async Task OnClientMessageReceived(SocketMessage message)
			{
				// Check if channel is announcements channel or if it's a slash command
				if (message.Channel is not SocketNewsChannel newsChannel || message.Interaction != null) return;

				using DiamondContext db = new DiamondContext();

				// Check if channel is being tracked
				if (!db.AutoPublisherChannels.Where(pc => pc.ChannelId == newsChannel.Id).Any()) return;

				// Publish the message
				await (message as IUserMessage).CrosspostAsync();
			}
		}
	}
}