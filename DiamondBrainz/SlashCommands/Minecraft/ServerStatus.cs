using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.API.Schemes.Mcsrvstat;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Minecraft
{
    public partial class Minecraft
	{
		[DSlashCommand("server-status", "Get a server status.")]
		public async Task ServerStatusCommandAsync(
			[Summary("hostname", "The IP/hostname of the server.")] string hostname,
			[Summary("port", "The port of the server. Default is 25565 for Java and 19132 for Bedrock.")] short? port = null,
			[Summary("server-platform", "The platform of the server. Default is Java.")] ServerPlatform platform = ServerPlatform.Java,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			// Get the server status
			ServerStatus serverStatus = this._mcsrvstatApi.GetServerStatus(hostname, port, platform == ServerPlatform.Bedrock);

			// Default embed
			DefaultEmbed embed = new DefaultEmbed("Minecraft Server Status", "🧊", this.Context)
			{
				Title = $"{serverStatus.Hostname}:{serverStatus.Port}"
			};

			// Return case the server is offline or not found
			if (!serverStatus.IsOnline)
			{
				embed.Description = "The server is either offline or does not exist.";
				_ = await embed.SendAsync();
				return;
			}

			// Motd string
			StringBuilder motdSb = new StringBuilder();
			foreach (string motdLine in serverStatus.Motd.CleanMotd)
			{
				if (motdSb.Length > 0)
				{
					_ = motdSb.Append("\n");
				}
				_ = motdSb.Append(motdLine.Replace(@"\", @"\\").Replace("*", @"\*").Replace("_", @"\_"));
			}
			// Players string
			StringBuilder playersSb = null;
			if (serverStatus.Players.OnlinePlayersNames != null)
			{
				playersSb = new StringBuilder();
				foreach (string playerName in serverStatus.Players.OnlinePlayersNames)
				{
					if (playersSb.Length > 0)
					{
						_ = playersSb.Append(", ");
					}
					_ = playersSb.Append(playerName);
				}
			}

			// First row
			_ = embed.AddField("📣 MOTD", motdSb.ToString());
			// Second row
			_ = embed.AddField("📊 Player Count", $"**{serverStatus.Players.OnlinePlayersCount}**/{serverStatus.Players.MaxPlayers}", true);
			_ = embed.AddField("🏷 Server Version", $"{serverStatus.Version} ({serverStatus.Protocol})", true);
			_ = embed.AddField("🖥 Software", serverStatus.Software ?? "Unknown", true);
			// Third row
			_ = embed.AddField("👥 Players", playersSb != null ? playersSb.ToString() : "None or hidden");
			// Fourth row
			if (serverStatus.Plugins != null)
			{
				StringBuilder pluginsSb = new StringBuilder();
				foreach (string pluginName in serverStatus.Plugins.Names)
				{
					if (pluginsSb.Length > 0)
					{
						_ = pluginsSb.Append(", ");
					}
					_ = pluginsSb.Append(pluginName);
				}
				_ = embed.AddField("🧩 Plugins", pluginsSb.ToString());
			}
			// Fifth row
			if (serverStatus.Mods != null)
			{
				StringBuilder modsSb = new StringBuilder();
				foreach (string modName in serverStatus.Mods.Names)
				{
					if (modsSb.Length > 0)
					{ _ = modsSb.Append(", "); }
					_ = modsSb.Append(modName);
				}
				_ = embed.AddField("🔫 Mods", modsSb.ToString());
			}

			// Send with Thumbnail
			if (serverStatus.IconBase64 != null)
			{
				string iconBase64 = serverStatus.IconBase64.Replace("data:image/png;base64,", "");
				byte[] iconBytes = Convert.FromBase64String(iconBase64);
				using (Stream stream = new MemoryStream(iconBytes))
				{
					// Send the embed
					string fileName = $"serverStatus_{serverStatus.Hostname}_{serverStatus.Port}.png";
					_ = embed.WithThumbnailUrl($"attachment://{fileName}");

					_ = await base.FollowupWithFileAsync(stream, fileName, embed: embed.Build());
				}
			}
			else
			{
				_ = await embed.SendAsync();
			}
		}

		public enum ServerPlatform
		{
			Java,
			Bedrock,
		}
	}
}
