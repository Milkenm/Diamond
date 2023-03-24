using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using ScriptsLibV2.Util;

namespace Diamond.Brainz.Commands
{
	public partial class DiscordModule : ModuleBase<SocketCommandContext>
	{
		[Name("Emoji"), Command("emoji"), Alias("twemoji"), Summary("Gets the provided emoji.")]
		public async Task Emoji(string emoji)
		{
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("Twemoji", TwemojiUtils.GetEmojiUrlFromEmoji("😃"));
			embed.WithImageUrl(TwemojiUtils.GetEmojiUrlFromEmoji(emoji));

			await this.ReplyAsync(embed: embed.FinishEmbed(this.Context)).ConfigureAwait(false);
		}
	}
}