using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.ScriptsLib.DiscordBot;
using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.Moderation
{
	public class DownloadEmojis : ISlashCommand
	{
		private const int AverageEmojiSize = 20_000;
		private const int AverageGifSize = 125_000;

		protected override void SlashCommandBuilder()
		{
			Name = "downloademojis";
			Description = "Downloads the custom emojis from a server.";
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			SocketGuild guild = client.GetGuild((ulong)command.GuildId);
			IReadOnlyCollection<GuildEmote> guildEmotes = guild.Emotes;

			DefaultEmbed embed = GetBaseEmbed(command);

			if (guildEmotes.Count == 0)
			{
				embed.WithDescription(":x: This server has no custom emojis.");
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

			embed.AddField("🙂 Emojis", emojisCount, true);
			embed.AddField("😼 GIF Emojis", gifsCount, true);
			embed.AddField("📁 Expected Size", Utils.ByteSizeToString((double)((emojisCount * AverageEmojiSize) + (gifsCount * AverageGifSize))), true);
			embed.WithDescription("Retrieving custom emojis. This may take a while...");
			await embed.SendAsync();

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

			await command.ModifyOriginalResponseAsync(new Action<MessageProperties>((messageProperties) =>
			{
				messageProperties.Embeds = null;

				DefaultEmbed embed = GetBaseEmbed(command);
				embed.AddField("🙂 Emojis", emojisCount, true);
				embed.AddField("😼 GIF Emojis", gifsCount, true);
				embed.AddField("📁 Size", Utils.ByteSizeToString((double)compressedFileStream.Length), true);

				messageProperties.Embed = embed.FinishEmbed(command);
			}));
			await command.FollowupWithFileAsync(new FileAttachment(compressedFileStream, $"{guild.Name.Replace(' ', '_')}_Emotes.zip"));
			compressedFileStream.Close();
		}

		private DefaultEmbed GetBaseEmbed(SocketSlashCommand command)
		{
			return new DefaultEmbed("Download Emojis", "😀", command);
		}
	}
}
