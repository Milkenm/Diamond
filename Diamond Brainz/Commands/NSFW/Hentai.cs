﻿using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using Newtonsoft.Json;

using ScriptsLib.Network;

using System.Threading.Tasks;

// # = #
// https://api.computerfreaker.cf/v1/hentai
// # = #

namespace Diamond.Brainz.Commands
{
	public partial class NSFWModule : ModuleBase<SocketCommandContext>
	{
		[Name("Hentai"), Command("hentai"), Summary("Gives you a NSFW image"), RequireNsfw(ErrorMessage = "You must be on an NSFW channel to use this command.")]
		public async Task Hentai()
		{
			// GET IMAGE
			string response = Requests.GET("https://nekos.life/api/v2/img/hentai");
			NekosLifeResponse resp = JsonConvert.DeserializeObject<NekosLifeResponse>(response);

			// CREATE THE EMBED
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("NSFW", Twemoji.GetEmojiUrlFromEmoji("💮"));
			embed.WithImageUrl(resp.URL);

			// REPLY
			await this.ReplyAsync(embed: Embeds.FinishEmbed(embed, this.Context)).ConfigureAwait(false);
		}

		private class NekosLifeResponse
		{
			[JsonProperty("url")] public string URL;
		}
	}
}
