using Diamond.Brainz.Data;

using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

using static Diamond.Brainz.Utils.Folders;

namespace Diamond.Brainz.Commands
{
	public partial class UnsortedModule : ModuleBase<SocketCommandContext>
	{
		[Name("Download Emojis"), Command("downloademojis"), Alias("de", "download", "emojis", "downloademotes", "download emojis", "download emotes", "downloadcustomemotes", "download custom emotes", "dce", "downloadcustomemojis", "download custom emojis"), Summary("Download every custom emoji on the current server to a ZIP and uploads it to the channel.")]
		public async Task DownloadEmojis()
		{
			if (!(Context.Channel is IPrivateChannel))
			{
				IReadOnlyCollection<GuildEmote> emotes = Context.Guild.Emotes;

				if (emotes.Count > 0)
				{
					await ReplyAsync("Retrieving custom emojis. This may take a while...").ConfigureAwait(false);

					string path = GlobalData.Folders[EFolder.Temp].Path + $"{Context.Guild.Id}";

					Directory.CreateDirectory($@"{path}\");

					using (WebClient wc = new WebClient())
					{
						foreach (GuildEmote emote in emotes)
						{
							string extension = emote.Animated ? "gif" : "png";
							wc.DownloadFile(emote.Url, $@"{path}\{emote.Name}.{extension}");
						}
					}

					ZipFile.CreateFromDirectory($@"{path}\", $"{path}.zip");

					await Context.Channel.SendFileAsync($"{path}.zip").ConfigureAwait(false);

					Directory.Delete($@"{path}\", true);
					File.Delete($"{path}.zip");
				}
				else
				{
					throw new Exception("This server has no custom emojis.");
				}
			}
			else
			{
				throw new Exception("This command can only be used on servers!");
			}
		}
	}
}
