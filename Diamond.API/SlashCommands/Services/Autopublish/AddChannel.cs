using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.AutoPublisher;

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
				DefaultEmbed embed = this.DefaultEmbed;

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
				_ = db.AutoPublisherChannels.Add(new DbAutoPublisherChannel()
				{
					GuildId = announcementsChannel.Guild.Id,
					ChannelId = announcementsChannel.Id,
					TrackingSinceUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
					AddedByUserId = this.Context.User.Id,
				});
				await db.SaveAsync();

				// Send existing messages button
				_ = embed.AddButton("Publish the latest 5 messages", $"{AutoPublisherComponentIds.BUTTON_AUTOPUBLISHER_PUBLISH_MESSAGES}:{announcementsChannel.Id}", ButtonStyle.Primary, Emoji.Parse("📣"));

				// Send success embed
				embed.Title = "Channel added!";
				embed.Description = $"Now tracking {announcementsChannel.Mention}!\nNew messages in this channel will be automatically published.";
				_ = await embed.SendAsync();

				// Set permissions
				bool success = await SetPermissionsAsync(this._client, announcementsChannel);
				if (!success)
				{
					await SendSetPermissionsErrorEmbedAsync(this.DefaultEmbed, announcementsChannel);
				}
			}

			[ComponentInteraction($"{AutoPublisherComponentIds.BUTTON_AUTOPUBLISHER_PUBLISH_MESSAGES}:*", true)]
			public async Task ButtonPublishExistingHandler(ulong channelId)
			{
				await this.DeferAsync();

				// Remove the button from the embed
				_ = await this.ModifyOriginalResponseAsync((msg) =>
				{
					msg.Components = new ComponentBuilder().Build();
				});

				using DiamondContext db = new DiamondContext();

				// Check if the channel still exists in the database
				DbAutoPublisherChannel pc = db.AutoPublisherChannels.Where(pc => pc.ChannelId == channelId).FirstOrDefault();
				if (pc != null)
				{
					// Publish old messages
					await this.PublishOldMessagesInChannelAsync(pc, 5, false);
				}
			}

			private static async Task<bool> SetPermissionsAsync(DiamondClient client, SocketNewsChannel announcementsChannel)
			{
				// Check if permission is already set
				OverwritePermissions? existingOverwrites = announcementsChannel.GetPermissionOverwrite(client.CurrentUser);
				if (existingOverwrites != null)
				{
					if (((OverwritePermissions)existingOverwrites).SendMessages == PermValue.Allow)
					{
						return true;
					}
				}
				// Set the permission
				try
				{
					await announcementsChannel.AddPermissionOverwriteAsync(client.CurrentUser, new OverwritePermissions(sendMessages: PermValue.Allow));
					return true;
				}
				catch
				{
					return false;
				}
			}

			private static async Task SendSetPermissionsErrorEmbedAsync(DefaultEmbed defaultEmbed, SocketNewsChannel announcementsChannel)
			{
				defaultEmbed.Title = "Error setting permissions";
				defaultEmbed.Description = $"I couldn't set the **{ChannelPermission.SendMessages}** permission for the {announcementsChannel.Mention} channel.\nThis permission is needed for me to publish the messages.\nIt cannot be given through a role. This only works by setting the permission specifically for me in the channel's permissions tab (Discord stuff...).\n\nPlease give me the **{ChannelPermission.SendMessages}** permission on the {announcementsChannel.Mention} channel or give me the **{GuildPermission.ManageRoles}** permission on the guild so I can set it myself.\nIf it still doesn't work, it's probably because you have a permission denying **{ChannelPermission.SendMessages}** and I can't override it. For that I need the **{GuildPermission.Administrator}** permission.";
				_ = defaultEmbed.AddButton("Retry", $"{AutoPublisherComponentIds.BUTTON_AUTOPUBLISHER_PERMISSIONS_SET_RETRY}:{announcementsChannel.Id}", ButtonStyle.Primary, Emoji.Parse("🔁"))
					.AddButton("I'll do it myself", AutoPublisherComponentIds.BUTTON_AUTOPUBLISHER_PERMISSIONS_SET_CLOSE, ButtonStyle.Secondary, Emoji.Parse("💪"));
				_ = await defaultEmbed.SendAsync(true, true);
			}

			[ComponentInteraction($"{AutoPublisherComponentIds.BUTTON_AUTOPUBLISHER_PERMISSIONS_SET_RETRY}:*", true)]
			public async Task ButtonPublishRetryHandlerAsync(ulong channelId)
			{
				await this.DeferAsync();

				// Get the channel
				SocketGuildChannel channel = this.Context.Guild.GetChannel(channelId);
				if (channel == null || channel is not SocketNewsChannel newsChannel)
				{
					await Utils.DeleteResponseAsync(this.Context);
					return;
				}

				// Set permissions
				bool success = await SetPermissionsAsync(this._client, newsChannel);
				if (success)
				{
					await Utils.DeleteResponseAsync(this.Context);
				}
			}

			[ComponentInteraction(AutoPublisherComponentIds.BUTTON_AUTOPUBLISHER_PERMISSIONS_SET_CLOSE, true)]
			public async Task ButtonPublishCloseHandlerAsync()
			{
				await this.DeferAsync();

				await Utils.DeleteResponseAsync(this.Context);
			}
		}
	}
}
