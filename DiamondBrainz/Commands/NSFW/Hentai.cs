using System.Threading.Tasks;

using Diamond.API.Classes;

using Discord;
using Discord.Commands;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

// # = #
// https://api.computerfreaker.cf/v1/hentai
// # = #

namespace Diamond.API.Commands
{
	public partial class NSFWModule : ModuleBase<SocketCommandContext>
	{
		[Name("Hentai"), Command("hentai"), Summary("Gives you a NSFW image"), RequireNsfw(ErrorMessage = "You must be on an NSFW channel to use this command.")]
		public async Task Hentai()
		{
			// GET IMAGE
			string response = RequestUtils.Get("https://nekos.life/api/v2/img/hentai");
			NekosLifeResponse resp = JsonConvert.DeserializeObject<NekosLifeResponse>(response);

			// CREATE THE EMBED
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("NSFW", TwemojiUtils.GetUrlFromEmoji("💮"));
			embed.WithImageUrl(resp.URL);

			// REPLY
			await this.ReplyAsync(embed: embed.FinishEmbed(this.Context)).ConfigureAwait(false);
		}
	}
}
