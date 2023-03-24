using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using ScriptsLibV2.Util;

namespace Diamond.Brainz.Commands
{
	public partial class ToolsModule : ModuleBase<SocketCommandContext>
	{
		[Name("Random Number Generator"), Command("random number"), Alias("rnum", "rand num", "rand number", "random num", "rn", "random", "rnd", "rand"), Summary("Generates a random number between min and max numbers.")]
		public async Task GenerateRandomNumber(int min, int max)
		{
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("Random Number Generator", TwemojiUtils.GetEmojiUrlFromEmoji("🎲"));

			if (min < max)
			{
				int number = new Random().Next(min, max + 1);

				embed.AddField("**Generated Number**", number.ToString());
			}
			else
			{
				embed.WithDescription("**❌ Error:** Invalid numbers.");
			}

			await this.ReplyAsync(embed: embed.FinishEmbed(this.Context)).ConfigureAwait(false);
		}
	}
}