using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Data;
using Diamond.API.Util;

using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Services
{
	public partial class Services
	{
		public partial class AutoPublish
		{
			[DSlashCommand("list-channels", "List all channels currently being tracked.")]
			public async Task ListChannelsCommandAsync(
				[ShowEveryone] bool showEveryone = false
			)
			{
				await this.DeferAsync(!showEveryone);

				using DiamondContext db = new DiamondContext();

				DefaultEmbed embed = this.DefaultEmbed;

				List<PublishChannel> trackedChannels = db.AutoPublisherChannels.Where(pc => pc.GuildId == this.Context.Guild.Id).ToList();

				StringBuilder channelsSb = new StringBuilder();
				int trackingChannelsCount = 0;
				foreach (PublishChannel pc in trackedChannels)
				{
					// Check if the channel exists. If it doesn't exist, delete it from the database
					SocketGuildChannel? channel = this.Context.Guild.GetChannel(pc.ChannelId);
					if (channel == null)
					{
						_ = db.AutoPublisherChannels.Remove(pc);
						continue;
					}

					trackingChannelsCount++;

					// Get user + timestamp block
					SocketGuildUser? user = this.Context.Guild.GetUser(pc.AddedByUserId);
					string addedBy = $"{(user != null ? user.Mention : "<unknown>")} at {Utils.GetTimestampBlock(pc.TrackingSinceUnix)}";

					// Check if the announcements channel was turned into a text channel
					if (channel is not SocketNewsChannel newsChannel)
					{
						_ = channelsSb.Append($"{(channel as SocketTextChannel).Mention} (not being tracked) added by {addedBy}", "\n");
						continue;
					}
					_ = channelsSb.Append($"{newsChannel.Mention} added by {addedBy}", "\n");
				}

				// This is checked here instead of above the 'for' because there are channels that could have had been removed and would pass the 'null' check
				if (channelsSb.Length == 0)
				{
					embed.Title = "Nothing being tracked";
					embed.Description = "There are no announcement channels being tracked in this guild.";
					_ = await embed.SendAsync();
					return;
				}

				embed.Title = $"Currently tracking {trackingChannelsCount} announcement channels";
				embed.Description = channelsSb.ToString();
				_ = await embed.SendAsync();
			}
		}
	}
}
