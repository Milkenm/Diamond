using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Data;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Services
{
	public partial class Services
	{
		public partial class AutoPublish
		{
			[DSlashCommand("add-channel", "Select an announcements channel to automatically publish messages.")]
			public async Task AddChannelCommandAsync(
				[Summary("announcements-channel", "The channel to automatically publish messages.")] SocketNewsChannel announcementsChannel,
				[ShowEveryone] bool showEveryone = false
			)
			{
				await this.DeferAsync(!showEveryone);

				using DiamondContext db = new DiamondContext();

				// Create the base embed
				DefaultEmbed embed = new DefaultEmbed("Auto Publisher", "📣", this.Context);

				// Check if channel already exists
				if (db.AutoPublisherChannels.Where(pc => pc.ChannelId == announcementsChannel.Id).Any())
				{
					// Send error embed
					embed.Title = "Channel already added";
					embed.Description = $"The channel {announcementsChannel.Mention} is already being tracked!";
					_ = await embed.SendAsync();
					return;
				}

				// Save channel to database
				_ = db.AutoPublisherChannels.Add(new PublishChannel()
				{
					GuildId = announcementsChannel.Guild.Id,
					ChannelId = announcementsChannel.Id,
				});
				await db.SaveAsync();

				// Send success embed
				embed.Title = "Channel added!";
				embed.Description = $"Now tracking {announcementsChannel.Mention}!\nNew messages in this channel will be automatically published.";
				_ = await embed.SendAsync();
			}

			private async Task OnClientMessageReceived(SocketMessage message)
			{
				// Check if channel is announcements channel
				if (message.Channel is not SocketNewsChannel newsChannel) return;

				using DiamondContext db = new DiamondContext();

				// Check if channel is being tracked
				if (!db.AutoPublisherChannels.Where(pc => pc.ChannelId == newsChannel.Id).Any()) return;

				// Publish the message
				await (message as IUserMessage).CrosspostAsync();
			}
		}
	}
}
