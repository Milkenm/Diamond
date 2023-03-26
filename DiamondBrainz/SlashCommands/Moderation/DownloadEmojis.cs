using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		protected override void SlashCommandBuilder()
		{
			Name = "downloademojis";
			Description = "Downloads the custom emojis from a server.";
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			SocketGuild guild = client.GetGuild((ulong)command.GuildId);
			IReadOnlyCollection<GuildEmote> emotes = guild.Emotes;

			EmbedBuilder embed = GetBaseEmbed();

			if (emotes.Count == 0)
			{
				embed.WithDescription("This server has no custom emojis.");
				return;
			}

			embed.WithDescription("Retrieving custom emojis. This may take a while...");
			await command.RespondAsync(embed: embed.FinishEmbed(command));

			Dictionary<GuildEmote, byte[]> emojisDictionary = new Dictionary<GuildEmote, byte[]>();
			using (WebClient wc = new WebClient())
			{
				foreach (GuildEmote guildEmoji in emotes)
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
					ZipArchiveEntry zipEntry = zipArchive.CreateEntry(emojiPair.Key.Name);
					Debug.WriteLine("zipentry: " + zipEntry.Length);

					using (MemoryStream emojiFileStream = new MemoryStream(emojiPair.Value))
					using (Stream zipEntryStream = zipEntry.Open())
					{
						emojiFileStream.CopyTo(zipEntryStream);
						Debug.WriteLine("zipentrystream: " + emojiFileStream.Length + " | " + emojiFileStream.Length);
					}
				}
			}

			await command.ModifyOriginalResponseAsync(new Action<MessageProperties>((messageProperties) =>
			{
				messageProperties.Embeds = null;

				EmbedBuilder embed = GetBaseEmbed();
				embed.AddField("Count", emotes.Count, true);
				embed.AddField("Size", compressedFileStream.Length, true);

				messageProperties.Embed = embed.FinishEmbed(command);
			}));
			await command.FollowupWithFileAsync(new FileAttachment(compressedFileStream, $"{guild.Name.Replace(' ', '_')}_Emotes.zip"));
			compressedFileStream.Close();
		}

		private EmbedBuilder GetBaseEmbed()
		{
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("Download Emojis", TwemojiUtils.GetUrlFromEmoji("😀"));
			return embed;
		}
	}
}
