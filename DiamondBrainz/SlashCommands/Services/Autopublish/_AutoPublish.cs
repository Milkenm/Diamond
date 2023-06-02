using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.Rest;
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

				Debug.WriteLine("a");

				// Initialize events
				if (!_eventInitialized)
				{
					_eventInitialized = true;

					this._client.MessageReceived += this.OnClientMessageReceived;
					// Publish messages sent while the bot was offline
					_ = this.PublishOldMessages().GetAwaiter();
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

			private async Task PublishOldMessages()
			{
				DiamondContext db = new DiamondContext();

				foreach (PublishChannel pc in db.AutoPublisherChannels)
				{
					// Get channel
					IChannel channel = await this._client.GetChannelAsync(pc.ChannelId);

					// Check if channel exists
					if (channel == null)
					{
						_ = db.AutoPublisherChannels.Remove(pc);
						await db.SaveAsync();
						continue;
					}

					// Check if channel is a news channel
					if (channel is not SocketNewsChannel newsChannel) continue;

					// Get last 10 messages from channel
					IEnumerable<IMessage> messages = (await newsChannel.GetMessagesAsync(10).FlattenAsync()).Where(msg => msg.CreatedAt.ToUnixTimeSeconds() >= pc.TrackingSinceUnix).Reverse();

					// Check if all messages are published
					foreach (IMessage message in messages)
					{
						RestUserMessage msg = (RestUserMessage)message;

						// Check if the message is already published
						MessageFlags? flags = msg.Flags;
						if (flags != null && flags.Value == MessageFlags.Crossposted) continue;

						// Publish the message
						await msg.CrosspostAsync();
					}
				}
			}
		}
	}
}