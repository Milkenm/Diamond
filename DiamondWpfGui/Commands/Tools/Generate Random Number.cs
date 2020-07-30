using Discord;
using Discord.Commands;

using System;
using System.Threading.Tasks;
using Diamond.WPF.Utils;

namespace Diamond.WPF.Commands
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Name("Random Number Generator"), Command("random number"), Alias("rnum", "rand num", "rand number", "random num", "rn", "random", "rnd", "rand"), Summary("Generates a random number between min and max numbers.")]
		public async Task GenerateRandomNumber(int min, int max)
		{
            int number = new Random().Next(min, max);

            EmbedBuilder embed = new EmbedBuilder()
            {
                Title = "Random Number Generator",
                Description = "Generated Number: " + number.ToString(),
            };

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
		}
	}
}