using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands
{
    public class DownloadEmojis : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly DiamondClient _client;

		public DownloadEmojis(DiamondClient client)
		{
			this._client = client;
		}

		[EnabledInDm(false)]
		[DSlashCommand("download-emojis", "Downloads the custom emojis from a server.")]
		public async Task DownloadEmojisCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			SocketGuild guild = this._client.GetGuild(this.Context.Guild.Id);
			IReadOnlyCollection<GuildEmote> guildEmotes = guild.Emotes;

			DefaultEmbed embed = new DefaultEmbed("Download Emojis", "😀", this.Context);

			if (guildEmotes.Count == 0)
			{
				embed.WithDescription("❌ This server has no custom emojis.");
				return;
			}

			int emojisCount = 0, gifsCount = 0;
			foreach (GuildEmote guildEmote in guildEmotes)
			{
				if (guildEmote.Animated)
				{
					gifsCount++;
				}
				else
				{
					emojisCount++;
				}
			}

			Dictionary<GuildEmote, byte[]> emojisDictionary = new Dictionary<GuildEmote, byte[]>();
			using (WebClient wc = new WebClient())
			{
				foreach (GuildEmote guildEmoji in guildEmotes)
				{
					byte[] emojiBytes = wc.DownloadData(new Uri(guildEmoji.Url));
					emojisDictionary.Add(guildEmoji, emojiBytes);
				}
			}

			MemoryStream compressedFileStream = new MemoryStream();
			using (ZipArchive zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Update, true))
			{
				foreach (KeyValuePair<GuildEmote, byte[]> emojiPair in emojisDictionary)
				{
					string extension = emojiPair.Key.Animated ? ".gif" : ".png";
					ZipArchiveEntry zipEntry = zipArchive.CreateEntry(emojiPair.Key.Name + extension);

					using (MemoryStream emojiFileStream = new MemoryStream(emojiPair.Value))
					using (Stream zipEntryStream = zipEntry.Open())
					{
						emojiFileStream.CopyTo(zipEntryStream);
					}
				}
			}

			embed.Description = "Custom emojis retrieved!";
			embed.AddField("🙂 Emojis", emojisCount, true);
			embed.AddField("😼 GIF Emojis", gifsCount, true);
			embed.AddField("📁 Size", ScriptsLibV2.Util.Utils.ByteSizeToString(compressedFileStream.Length), true);
			await embed.SendAsync();

			await this.Context.Interaction.FollowupWithFileAsync(new FileAttachment(compressedFileStream, $"{guild.Name.Replace(' ', '_')}_Emotes.zip"));
			compressedFileStream.Close();
		}
	}
}
