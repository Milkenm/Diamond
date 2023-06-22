using System.Collections.Generic;
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
			[DSlashCommand("remove-channel", "Stops tracking an announcements channel.")]
			public async Task RemoveChannelCommandAsync(
				[Summary("announcements-channel", "The announcements channel to stop tracking.")] SocketNewsChannel announcementsChannel,
				[ShowEveryone] bool showEveryone = false
			)
			{
				await this.DeferAsync(!showEveryone);

				using DiamondContext db = new DiamondContext();

				DefaultEmbed embed = this.DefaultEmbed;

				// Check if channel exists 
				IQueryable<DbAutoPublisherChannel> trackedChannel = db.AutoPublisherChannels.Where(pc => pc.ChannelId == announcementsChannel.Id);
				if (!trackedChannel.Any())
				{
					embed.Title = "That channel is not being tracked";
					embed.Description = $"The channel {announcementsChannel.Mention} is not currently being tracked.";
					_ = await embed.SendAsync();
					return;
				}

				// Remove the channel from the database
				DbAutoPublisherChannel pc = trackedChannel.First();
				_ = db.AutoPublisherChannels.Remove(pc);
				await db.SaveAsync();

				// Send embed
				embed.Title = "Announcements channel untracked";
				embed.Description = $"The channel {announcementsChannel.Mention} is no longer being tracked.";
				_ = await embed.SendAsync();

				// Remove permissions
				bool success = await RemovePermissionsAsync(this._client, announcementsChannel);
				if (!success)
				{
					await this.SendPermissionsRemoveErrorEmbedAsync(this.DefaultEmbed, announcementsChannel);
				}
			}

			public static async Task<bool> RemovePermissionsAsync(DiamondClient client, SocketNewsChannel announcementsChannel)
			{
				// Remove the announcement channel permissions from the bot if "Send Messages" is the only permission it has
				OverwritePermissions? botPerms = announcementsChannel.GetPermissionOverwrite(client.CurrentUser);
				if (botPerms == null) return true;

				// Check if bot still has the permission set
				List<ChannelPermission> allowedPerms = ((OverwritePermissions)botPerms).ToAllowList();
				if (allowedPerms.Count != 1 || allowedPerms[0] != ChannelPermission.SendMessages) return true;

				try
				{
					await announcementsChannel.RemovePermissionOverwriteAsync(client.CurrentUser);
					return true;
				}
				catch
				{
					return false;
				}
			}

			public async Task SendPermissionsRemoveErrorEmbedAsync(DefaultEmbed embed, SocketNewsChannel announcementsChannel)
			{
				embed.Title = "Error removing permissions";
				embed.Description = $"I couldn't remove the **{ChannelPermission.SendMessages}** permission from the {announcementsChannel.Mention} channel.\nThis permission was needed for me to publish the messages but is probably no longer needed.\n\nIf you want, you can remove the **{ChannelPermission.SendMessages}** permission from me on the {announcementsChannel.Mention} channel or give me the **{GuildPermission.ManageRoles}** permission on the guild so I can set it myself (if it still doesn't work, I'll need the **{GuildPermission.Administrator}** permission to remove it because you probably have a permission denying **{ChannelPermission.SendMessages}**).";
				MessageComponent components = new ComponentBuilder()
					.WithButton("Retry", $"button_autopublish_removepermissions_retry:{announcementsChannel.Id}", ButtonStyle.Primary, Emoji.Parse("🔁"))
					.WithButton("It's ok", $"button_autopublish_removepermissions_close", ButtonStyle.Secondary, Emoji.Parse("😊"))
					.Build();
				embed.Component = components;
				_ = await embed.SendAsync(true, true);
			}

			[ComponentInteraction("button_autopublish_removepermissions_retry:*", true)]
			public async Task ButtonRetryUnsetPermissionsHandlerAsync(ulong channelId)
			{
				await this.DeferAsync();

				// Get channel
				SocketGuildChannel channel = this.Context.Guild.GetChannel(channelId);
				if (channel == null || channel is not SocketNewsChannel newsChannel)
				{
					await Utils.DeleteResponseAsync(this.Context);
					return;
				}

				// Remove permissions
				bool success = await RemovePermissionsAsync(this._client, newsChannel);
				if (success)
				{
					await Utils.DeleteResponseAsync(this.Context);
				}
			}

			[ComponentInteraction("button_autopublish_removepermissions_cancel", true)]
			public async Task ButtonCancelUnsetPermissionsHandlerAsync()
			{
				await this.DeferAsync();

				await Utils.DeleteResponseAsync(this.Context);
			}
		}
	}
}