﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Server
{
	public partial class Server
	{
		private static readonly Dictionary<UserStatus, string> _statusMap = new Dictionary<UserStatus, string>()
		{
			{ UserStatus.Online, "<:discord_online_icon:1112230665967112282> Online" },
			{ UserStatus.Idle, "<:discord_away_icon:1112230660426448986> Idle" },
			{ UserStatus.DoNotDisturb, "<:discord_busy_icon:1112230662833975407> Do Not Disturb" },
			{ UserStatus.Invisible, "<:discord_offline_icon:1112230664310378537> Invisible" },
			{ UserStatus.AFK, "<:discord_away_icon:1112230660426448986> AFK" },
			{ UserStatus.Offline, "<:discord_offline_icon:1112230664310378537> Offline" },
		};

		[DSlashCommand("user", "Show info about a server user.")]
		public async Task ServerUserCommandAsync(
			[Summary("user", "The user to view info about.")] SocketGuildUser user,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.Context.Guild.DownloadUsersAsync();

			// Get all user roles into a string
			StringBuilder rolesSb = new StringBuilder();
			foreach (SocketRole role in user.Roles)
			{
				// Ignore the @everyone role (it's a role apparently)
				if (role.IsEveryone) continue;

				if (rolesSb.Length > 0)
				{
					_ = rolesSb.Append(", ");
				}
				_ = rolesSb.Append(role.Mention);
			}
			// Get all user guild permissions into a string
			StringBuilder permissionsSb = new StringBuilder();
			foreach (GuildPermission permission in user.GuildPermissions.ToList())
			{
				if (permissionsSb.Length > 0)
				{
					_ = permissionsSb.Append(", ");
				}
				_ = permissionsSb.Append(permission.ToString());
			}
			// Get all user clients into a string
			StringBuilder clientsSb = new StringBuilder();
			foreach (ClientType client in user.ActiveClients)
			{
				if (clientsSb.Length > 0)
				{
					_ = clientsSb.Append(", ");
				}
				_ = clientsSb.Append(client.ToString());
			}

			DefaultEmbed embed = new DefaultEmbed("Guild User", "👤", this.Context)
			{
				Title = user.DisplayName,
				Description = user.Mention,
				ThumbnailUrl = user.GetDisplayAvatarUrl(),
			};

			// First row
			_ = embed.AddField("📛 Real name", $"{user.Username}#{user.DiscriminatorValue}", true);
			_ = embed.AddField("🛏️ Status", _statusMap[user.Status], true);
			_ = embed.AddField("Clients", clientsSb.Length > 0 ? clientsSb.ToString() : "None", true);
			// Second row
			_ = embed.AddField("📆 Created at", user.CreatedAt.ToString("dd/MM/yyyy, HH:mm:ss"), true);
			_ = embed.AddField("📆 Joined at", user.JoinedAt?.ToString("dd/MM/yyyy, HH:mm:ss"), true);
			_ = embed.AddField("🚀 Server Boosting since", user.PremiumSince != null ? user.PremiumSince?.ToString("dd/MM/yyyy, HH:mm:ss") : "Currently not boosting", true);
			// Third row
			_ = embed.AddField("🏷 Roles", rolesSb.ToString());
			// Fourth row
			_ = embed.AddField("👑 Permissions", permissionsSb.ToString());
			// Fifth row

			_ = await embed.SendAsync();
		}
	}
}